using System;
using System.Threading;

namespace Linq2d.Tests.Vectorization
{
    internal class VectorizationStateFixture: IDisposable
    {
        private static Mutex _mutex = new(false);
        public VectorizationStateFixture() => _mutex.WaitOne();

        public virtual void Dispose() => _mutex.ReleaseMutex();
    }
}
