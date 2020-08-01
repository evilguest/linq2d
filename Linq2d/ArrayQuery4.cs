using System.Linq.Expressions;

namespace Linq2d
{
    internal abstract class ArrayQuery4<R1, R2, R3, R4> : ArrayQueryBase
    {
        protected ArrayQuery4(ArraySource source, LambdaExpression kernel, R1 initValue1) : base(source, kernel, initValue1) { }

        protected ArrayQuery4(IArrayQuery sources, LambdaExpression kernel, R1 initValue1) : base(sources, kernel, initValue1) { }
        protected ArrayQuery4(IArrayQueryRecurrentHalf sources, LambdaExpression kernel, R2 initValue2) : base((IArrayQuery)sources, kernel)
            => ResultReplacements.Add(initValue2);

        protected ArrayQuery4(ArraySource source, LambdaExpression kernel) : base(source, kernel) { }
        protected ArrayQuery4(IArrayQuery sources, LambdaExpression kernel) : base(sources, kernel) { }
        protected ArrayQuery4(ArraySource left, ArraySource right, LambdaExpression kernel) : base(left, right, kernel) { }
        protected ArrayQuery4(IArrayQuery sources, ArraySource right, LambdaExpression kernel) : base(sources, right, kernel) { }

        protected (R1[,], R2[,], R3[,], R4[,])? _result;
        protected abstract (R1[,], R2[,], R3[,], R4[,]) GetResult();
        public (R1[,], R2[,], R3[,], R4[,]) ToArrays()
        {
            if (!_result.HasValue)
                _result = GetResult();
            return _result.Value;
        }
    }
}
