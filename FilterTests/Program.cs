using System;
using System.Diagnostics;
using System.Linq;


using System.Linq.Processing2d;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Validators;
using static System.Math;
using System.Collections.Generic;
using BenchmarkDotNet.Order;

namespace FilterTests
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class FilterBenchmark
    {
        [Params(1000)]
        public int W { get; set; }
        [Params(1000)]
        public int H { get; set; }

        [Params("C4", "C8")]
        public string Method { get; set; }

        [Params(
            typeof(BasicArrayFilter),
            typeof(UnsafeFilter),
            //typeof(DeferredLinqFilter),
            //typeof(FastLinqFilter),
            typeof(BestLinqFilter)
        )]
        [SameResult]
        public Type FilterType { get; set; }

        //public IEnumerable<Type> GetTestTypes() => from t in typeof(Program).Module.GetTypes()
        //                                           where typeof(IFilterTestSet<int>).IsAssignableFrom(t)
        //                                           select t;

        private Func<object> _filter;

        [GlobalSetup]
        public void Initialize()
        {
            var data = new int[H, W];
            var r = new Random(42);
            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data.GetLength(1); j++)
                    data[i, j] = r.Next(int.MaxValue / 8);

            var filterInstance = (IFilter<int>)Activator.CreateInstance(FilterType);
            filterInstance.Initialize(data);
            var m = FilterType.GetMethod(Method);
            _filter = (Func<object>)m.CreateDelegate(typeof(Func<object>), filterInstance);
        }

        [Benchmark]
        public object Filter() => _filter();
    }




    class Program
    {
        static IArray2d<int> Check(object arg0, IArray2d<int> arg1)
        {
            var r = new int[arg1.GetLength(0), arg1.GetLength(1)];
            return new ArrayWrapper<int>(r);
        }
        //static void Print<T>(IArray2d<T> matrix)
        //{
        //    var max = (from int m in matrix select Abs(m)).Max();
        //    var l = max == 0 ? 1 : (int)Ceiling(Log10(max));
        //    var format = "{0," + l + "} ";
        //    for (int i = 0; i < matrix.GetLength(0); ++i)
        //    {
        //        for (int j = 0; j <= matrix.GetLength(1); j++)
        //            Console.Write(format, matrix[i, j]);
        //        Console.WriteLine();
        //    }
        //    Console.WriteLine();
        //}

        static void Main(string[] args)
        {
            if (args.Any(s => s == "-wd") && !Debugger.IsAttached)
            {
                Console.WriteLine("Attach the debugger and press any key to continue...");
                Console.ReadKey();
            }

            var summary = BenchmarkRunner.Run<FilterBenchmark>(
                ManualConfig.Create(DefaultConfig.Instance)
                    .With(Array2dReturnValueValidator.FailOnError));

            if (Debugger.IsAttached)
                Console.ReadKey();

        }

    }
}