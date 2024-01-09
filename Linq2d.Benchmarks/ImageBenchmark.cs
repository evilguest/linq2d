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

        [Params("p00743.png", "p02652.png")]
        public string FileName { get; set; } = "p00743.png";
        public virtual void Initialize()
        {
            var fn = FileName;
            Console.WriteLine("Working at the directory '{0}'", Environment.CurrentDirectory);
            Console.WriteLine("Loading file {0}...", fn);
            _data = IO.ReadGrayScale8(FileName);
            Console.WriteLine("Loaded file {0}.", fn);
        }

    }
}