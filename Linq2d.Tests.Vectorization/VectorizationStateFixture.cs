using System;
using System.Threading;

namespace Linq2d.Tests.Vectorization
{
    internal class VectorizationStateFixture : IDisposable
    {
        private static bool busy = false;
        private static Mutex _mutex = new(false, "SIMD config");
        public VectorizationStateFixture()
        {
            if (busy)
                throw new Exception("Damn!");
            busy = true;
            Console.WriteLine($"{this.GetType().Name} is requesting the SIMD mutex...");
            _mutex.WaitOne();
            Console.WriteLine($"{this.GetType().Name} has got the SIMD mutex.");
        }

        public virtual void Dispose()
        {
            busy = false;
            _mutex.ReleaseMutex();
            Console.WriteLine($"{this.GetType().Name} has released the SIMD mutex.");
        }
    }
}
