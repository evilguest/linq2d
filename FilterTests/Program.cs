using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Processing2d.Expressions;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkHelpers;

namespace FilterTests
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class FilterBenchmark
    {
        [Params(10)]
        public int W { get; set; }
        [Params(10)]
        public int H { get; set; }

        [Params("C4")]
        public string Method { get; set; }

        [Params(
            //typeof(BasicArrayFilter),
            //typeof(UnsafeSIMDFilter),
            //typeof(UnsafeSIMDSpanFilter),
            typeof(UnsafeFilter),
            typeof(UnsafeAlternateFilter),
            //typeof(ExpressionLinqFilter),
            typeof(UnsafeParallelFilter)
            //typeof(DeferredLinqFilter),
            //typeof(FastLinqFilter),
            //typeof(BestLinqFilter)
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

            //var data = new int[,] { { 1, 2 }, { 3, 4 } };
            //var t = from d in data 
            //        from r in Recurrent.InitWith(0)
            //        select d + r[-1, 0] + r[0, -1] - r[-1, -1];
            var summary = BenchmarkRunner.Run<FilterBenchmark>(
                ManualConfig.Create(DefaultConfig.Instance)
                    .AddValidator(Array2dReturnValueValidator.FailOnError));

            if (Debugger.IsAttached)
                Console.ReadKey();

        }

    }
}