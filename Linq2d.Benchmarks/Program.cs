using BenchmarkDotNet.Running;
using System.Diagnostics;

namespace Linq2d.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!Debugger.IsAttached)
                BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            else
            {
                var c4 = new C4Benchmark();
                c4.Initialize();
                var r = c4.LinqC4();
            }
        }
    }
}
