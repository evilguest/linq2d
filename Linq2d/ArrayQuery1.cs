using System.Linq.Expressions;

namespace Linq2d
{
    internal abstract class ArrayQuery<R>: ArrayQueryBase
    {
        protected ArrayQuery(ArraySource source, LambdaExpression kernel, R initValue) : base(source, kernel, initValue) { }
        protected ArrayQuery(IArrayQuery sources, LambdaExpression kernel, R initValue) : base(sources, kernel, initValue) { }
        protected ArrayQuery(ArraySource source, LambdaExpression kernel) : base(source, kernel) { }
        protected ArrayQuery(IArrayQuery sources, LambdaExpression kernel) : base(sources, kernel) { }
        protected ArrayQuery(ArraySource left, ArraySource right, LambdaExpression kernel) : base(left, right, kernel) { }
        protected ArrayQuery(IArrayQuery sources, ArraySource right, LambdaExpression kernel) : base(sources, right, kernel) { }

        protected R[,] _result;
        protected abstract R[,] GetResult();
        public R[,] ToArray()
        {
            if (_result == null)
                _result = GetResult();
            return _result;
        }
    }
}
