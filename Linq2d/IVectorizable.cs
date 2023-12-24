using Linq2d.Expressions;
using System.Collections.Generic;

namespace Linq2d
{
    public interface IVectorizable
    {
        public bool Vectorized { get; }
        public IEnumerable<VectorizationResult> VectorizationResults { get; }
    }
}
