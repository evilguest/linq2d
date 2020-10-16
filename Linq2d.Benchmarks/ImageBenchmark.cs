using BenchmarkDotNet.Attributes;
using ImageHelpers;
using System;

namespace Linq2d.Benchmarks
{
    public class ImageBenchmark
    {

        protected byte[,] _data;
        protected Func<byte[,], int[,]> _integrateVector;
        protected Func<byte[,], int[,]> _integrateScalar;

        [Params("p00743.bmp.gz")]//, "p02652.bmp")]
        public string FileName { get; set; } = "p00743.bmp";
        public virtual void Initialize()
        {
            var fn = FileName;
            Console.WriteLine("Working at the directory '{0}'", Environment.CurrentDirectory);
            Console.WriteLine("Loading file {0}...", fn);
            var (data, palette) = IO.ReadImage(fn);
            _data = (from d in data select palette[d]).ToArray();
            Console.WriteLine("Loaded file {0}.", fn);
        }

    }
}