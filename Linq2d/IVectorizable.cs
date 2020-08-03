using Linq2d.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Linq2d
{
    public interface IVectorizable
    {
        bool Vectorized { get; }
        VectorizationResult VectorizationResult { get; }
    }
}
