using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Processing2d;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Parameters;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

namespace FilterTests
{
    public class Array2dReturnValueValidator : ExecutionValidatorBase
    {
        public static Array2dReturnValueValidator DontFailOnError { get; } = new Array2dReturnValueValidator(false);
        public static Array2dReturnValueValidator FailOnError { get; } = new Array2dReturnValueValidator(true);

        private Array2dReturnValueValidator(bool failOnError)
            : base(failOnError) { }

        internal static void FillMembers(object instance, BenchmarkCase benchmark)
        {
            var targetType = benchmark.Descriptor.Type;
            foreach (var parameter in benchmark.Parameters.Items)
            {
                var flags = BindingFlags.Public;
                flags |= parameter.IsStatic ? BindingFlags.Static : BindingFlags.Instance;

                object callInstance = instance;

                var paramProperty = targetType.GetProperty(parameter.Name, flags);

                if (paramProperty != null)
                {
                    var setter = paramProperty.GetSetMethod();
                    if (setter == null)
                        throw new InvalidOperationException(
                            $"Type {targetType.FullName}: no settable property {parameter.Name} found.");

                    if (setter.IsStatic)
                        callInstance = null;

                    setter.Invoke(callInstance, new[] { parameter.Value });
                    continue;
                }
                var paramField = targetType.GetField(parameter.Name, flags);
                if (paramField != null)
                {
                    if (paramField.IsStatic)
                        callInstance = null;
                    paramField.SetValue(callInstance, parameter.Value);
                    continue;
                }
                throw new InvalidOperationException($"Type {targetType.FullName}: no property or field {parameter.Name} found.");
            }

            var gs = targetType.GetMethods().Where(m => m.GetCustomAttribute<GlobalSetupAttribute>() != null).FirstOrDefault();
            if (gs != null)
                gs.Invoke(instance, new object[0]);
        }

        protected override void ExecuteBenchmarks(object benchmarkTypeInstance, IEnumerable<BenchmarkCase> benchmarks, List<ValidationError> errors)
        {
            foreach (var parameterGroup in benchmarks.GroupBy(i => i.Parameters, ParameterInstancesEqualityComparer.Instance(benchmarkTypeInstance)))
            {
                Console.WriteLine($"Evaluating param group '{parameterGroup.Key.DisplayInfo}'");

                var results = new List<(BenchmarkCase benchmark, object returnValue)>();
                bool hasErrorsInGroup = false;

                foreach (var benchmarkGroup in parameterGroup.Distinct(ParameterInstancesEqualityComparer.Instance(benchmarkTypeInstance)))
                {
                    try
                    {
                        FillMembers(benchmarkTypeInstance, benchmarkGroup);
                        Console.WriteLine($"Executing benchmark '{benchmarkGroup.DisplayInfo}'");

                        var result = benchmarkGroup.Descriptor.WorkloadMethod.Invoke(benchmarkTypeInstance, null);
                        if (benchmarkGroup.Descriptor.WorkloadMethod.ReturnType == typeof(void))
                            continue;

                        var resultType = result.GetType();

                        if (resultType.IsArray && resultType.GetArrayRank()==2)
                            result = Activator.CreateInstance(typeof(ArrayWrapper<int>).GetGenericTypeDefinition().MakeGenericType(resultType.GetElementType()), result);

                        results.Add((benchmarkGroup, result));
                    }
                    catch (Exception ex)
                    {
                        hasErrorsInGroup = true;

                        errors.Add(new ValidationError(
                            TreatsWarningsAsErrors,
                            $"Failed to execute benchmark '{benchmarkGroup.DisplayInfo}', exception was: '{GetDisplayExceptionMessage(ex)}'",
                            benchmarkGroup));
                        throw;
                    }
                }

                if (hasErrorsInGroup || results.Count == 0)
                    continue;


                foreach(var (benchmark, returnValue) in results.Where(result => !InDepthEqualityComparer.Instance.Equals(results[0].returnValue, result.returnValue)))
                {
                    errors.Add(new ValidationError(
                        TreatsWarningsAsErrors,
                        $"Inconsistent benchmark return value in {benchmark.DisplayInfo}"));
                }
            }
        }

        private class ParameterInstancesEqualityComparer : IEqualityComparer<ParameterInstances>, IEqualityComparer<BenchmarkCase>
        {
            private readonly Type _type;

            public static ParameterInstancesEqualityComparer Instance(object instance) => new ParameterInstancesEqualityComparer(instance);

            public ParameterInstancesEqualityComparer(object instance)
            {
                _type = instance.GetType();
            }


            public bool Equals(ParameterInstances x, ParameterInstances y)
            {
                if (ReferenceEquals(x, y))
                    return true;

                if (x == null || y == null)
                    return false;

                if (x.Count != y.Count)
                    return false;



                IEnumerable<ParameterInstance> xFiltered = x.Items.Where(IsGeneral);
                IEnumerable<ParameterInstance> yFiltered = y.Items.Where(IsGeneral);

                return xFiltered.OrderBy(i => i.Name).Zip(yFiltered.OrderBy(i => i.Name), (a, b) => a.Name == b.Name && Equals(a.Value, b.Value)).All(i => i);
            }

            private bool IsGeneral(ParameterInstance i) => (((MemberInfo)_type.GetProperty(i.Name) ?? _type.GetField(i.Name)).GetCustomAttributes<SameResultAttribute>().Count() == 0);

            public int GetHashCode(ParameterInstances obj)
            {
                if (obj.Count == 0)
                    return 0;

                unchecked
                {
                    int result = 0;

                    foreach (var paramInstance in obj.Items.Where(IsGeneral).OrderBy(i => i.Name))
                    {
                        result = (result * 397) ^ paramInstance.Name.GetHashCode();
                        result = (result * 397) ^ (paramInstance.Value?.GetHashCode() ?? 0);
                    }

                    return result;
                }
            }

            private bool IsSpecific(ParameterInstance i) => (((MemberInfo)_type.GetProperty(i.Name) ?? _type.GetField(i.Name)).GetCustomAttributes<SameResultAttribute>().Count() != 0);

            public bool Equals(BenchmarkCase x, BenchmarkCase y)
            {
                if (ReferenceEquals(x, y))
                    return true;

                if (x == null || y == null)
                    return false;

                if (x.Parameters.Count != y.Parameters.Count)
                    return false;


                if (x.Descriptor.WorkloadMethod != y.Descriptor.WorkloadMethod)
                    return false;

                IEnumerable<ParameterInstance> xFiltered = x.Parameters.Items.Where(IsSpecific);
                IEnumerable<ParameterInstance> yFiltered = y.Parameters.Items.Where(IsSpecific);

                return xFiltered.OrderBy(i => i.Name).Zip(yFiltered.OrderBy(i => i.Name), (a, b) => a.Name == b.Name && Equals(a.Value, b.Value)).All(i => i);
            }

            public int GetHashCode(BenchmarkCase obj)
            {
                int result = obj.Descriptor.WorkloadMethod.GetHashCode();
                unchecked
                {
                    foreach (var paramInstance in obj.Parameters.Items.Where(IsSpecific).OrderBy(i => i.Name))
                    {
                        result = (result * 397) ^ paramInstance.Name.GetHashCode();
                        result = (result * 397) ^ (paramInstance.Value?.GetHashCode() ?? 0);
                    }

                    return result;
                }
            }
        }

        private class InDepthEqualityComparer : IEqualityComparer
        {
            public static InDepthEqualityComparer Instance { get; } = new InDepthEqualityComparer();

            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            public new bool Equals(object x, object y)
            {
                if (ReferenceEquals(x, y) || object.Equals(x, y))
                    return true;

                if (x == null || y == null)
                    return false;

                return CompareEquatable(x, y) || CompareEquatable(y, x) || CompareStructural(x, y) || CompareStructural(y, x);
            }

            private static bool CompareEquatable(object x, object y)
            {
                var yType = y.GetType();

                var equatableInterface = x.GetType().GetInterfaces().FirstOrDefault(i => i.IsGenericType
                                                                                         && i.GetGenericTypeDefinition() == typeof(IEquatable<>)
                                                                                         && i.GetGenericArguments().Single().IsAssignableFrom(yType));

                if (equatableInterface == null)
                    return false;

                var method = equatableInterface.GetMethod(nameof(IEquatable<object>.Equals), BindingFlags.Public | BindingFlags.Instance);
                return (bool?)method?.Invoke(x, new[] { y }) ?? false;
            }

            private bool CompareStructural(object x, object y)
            {
                if (x is IStructuralEquatable xStructuralEquatable)
                    return xStructuralEquatable.Equals(y, this);

                var xArray = ToStructuralEquatable(x);
                var yArray = ToStructuralEquatable(y);

                if (xArray != null && yArray != null)
                    return Equals(xArray, yArray);

                return false;

                Array ToStructuralEquatable(object obj)
                {
                    switch (obj)
                    {
                        case Array array:
                            return array;

                        case IDictionary dict:
                            return dict.Keys.Cast<object>().OrderBy(k => k).Select(k => (k, dict[k])).ToArray();

                        case IEnumerable enumerable:
                            return enumerable.Cast<object>().ToArray();

                        default:
                            return null;
                    }
                }
            }

            public int GetHashCode(object obj) => StructuralComparisons.StructuralEqualityComparer.GetHashCode(obj);
        }
    }
    static partial class MoreEnumerable
    {
        /// <summary>
        /// Returns all distinct elements of the given source, where "distinctness"
        /// is determined via a projection and the default equality comparer for the projected type.
        /// </summary>
        /// <remarks>
        /// This operator uses deferred execution and streams the results, although
        /// a set of already-seen keys is retained. If a key is seen multiple times,
        /// only the first element with that key is returned.
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="keySelector">Projection for determining "distinctness"</param>
        /// <returns>A sequence consisting of distinct elements from the source sequence,
        /// comparing them by the specified key projection.</returns>

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.DistinctBy(keySelector, null);
        }

        /// <summary>
        /// Returns all distinct elements of the given source, where "distinctness"
        /// is determined via a projection and the specified comparer for the projected type.
        /// </summary>
        /// <remarks>
        /// This operator uses deferred execution and streams the results, although
        /// a set of already-seen keys is retained. If a key is seen multiple times,
        /// only the first element with that key is returned.
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="keySelector">Projection for determining "distinctness"</param>
        /// <param name="comparer">The equality comparer to use to determine whether or not keys are equal.
        /// If null, the default equality comparer for <c>TSource</c> is used.</param>
        /// <returns>A sequence consisting of distinct elements from the source sequence,
        /// comparing them by the specified key projection.</returns>

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return _(); IEnumerable<TSource> _()
            {
                var knownKeys = new HashSet<TKey>(comparer);
                foreach (var element in source)
                {
                    if (knownKeys.Add(keySelector(element)))
                        yield return element;
                }
            }
        }
    }
}