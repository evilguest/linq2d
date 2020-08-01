using System.Linq.Expressions;

namespace Linq2d
{
    internal abstract class ArrayQuery1<R>: ArrayQueryBase
    {
        protected ArrayQuery1(ArraySource source, LambdaExpression kernel, object initValue) : base(source, kernel, initValue) { }
        protected ArrayQuery1(IArrayQuery sources, LambdaExpression kernel, R initValue) : base(sources, kernel, initValue) { }
        protected ArrayQuery1(ArraySource source, LambdaExpression kernel) : base(source, kernel) { }
        protected ArrayQuery1(IArrayQuery sources, LambdaExpression kernel) : base(sources, kernel) { }
        protected ArrayQuery1(ArraySource left, ArraySource right, LambdaExpression kernel) : base(left, right, kernel) { }
        protected ArrayQuery1(IArrayQuery sources, ArraySource right, LambdaExpression kernel) : base(sources, right, kernel) { }

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
