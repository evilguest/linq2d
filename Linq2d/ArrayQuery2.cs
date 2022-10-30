using System.Linq.Expressions;

namespace Linq2d
{
    internal abstract class ArrayQuery2<R1, R2> : ArrayQueryBase
    {
        protected ArrayQuery2(ArraySource source, LambdaExpression kernel, R1 initValue1) : base(source, kernel, initValue1) { }
        protected ArrayQuery2(ArraySource source, LambdaExpression kernel, R2 initValue1) : base(source, kernel, initValue1) { }

        protected ArrayQuery2(IArrayQuery sources, LambdaExpression kernel, R1 initValue1) : base(sources, kernel, initValue1) { }
        protected ArrayQuery2(IArrayQuery sources, LambdaExpression kernel, R2 initValue2) : base(sources, kernel, initValue2) { }

        protected ArrayQuery2(ArraySource source, LambdaExpression kernel) : base(source, kernel) { }
        protected ArrayQuery2(IArrayQuery sources, LambdaExpression kernel) : base(sources, kernel) { }
        protected ArrayQuery2(ArraySource left, ArraySource right, LambdaExpression kernel) : base(left, right, kernel) { }
        protected ArrayQuery2(IArrayQuery sources, ArraySource right, LambdaExpression kernel) : base(sources, right, kernel) { }

        protected (R1[,], R2[,])? _result;
        protected abstract (R1[,], R2[,]) GetResult();
        public (R1[,], R2[,]) ToArrays()
        {
            if (!_result.HasValue)
                _result = GetResult();
            return _result.Value;
        }
    }
}
