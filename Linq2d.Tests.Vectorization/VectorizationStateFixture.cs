namespace Linq2d.Tests.Vectorization
{
    internal class VectorizationStateFixture : IDisposable
    {
        private static Mutex _mutex = new(false, "SIMD config");
        public VectorizationStateFixture()
        {
            Console.WriteLine($"{this.GetType().Name} is requesting the SIMD mutex...");
            _mutex.WaitOne();
            Console.WriteLine($"{this.GetType().Name} has got the SIMD mutex.");
        }

        public virtual void Dispose()
        {
            _mutex.ReleaseMutex();
            Console.WriteLine($"{this.GetType().Name} has released the SIMD mutex.");
        }
    }
}
