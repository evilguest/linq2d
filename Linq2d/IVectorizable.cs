using Linq2d.Expressions;

namespace Linq2d
{
    public interface IVectorizable
    {
        public VectorizationResult VectorizationResult { get; }
    }

    public interface IVectorizable2
    {
        public (VectorizationResult, VectorizationResult) VectorizationResults { get; }
    }

    public interface IVectorizable3
    {
        public (VectorizationResult, VectorizationResult, VectorizationResult) VectorizationResults { get; }
    }

    public interface IVectorizable4
    {
        public (VectorizationResult, VectorizationResult, VectorizationResult, VectorizationResult) VectorizationResults { get; }
    }

}
