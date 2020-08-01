using System;
using System.Linq.Expressions;

namespace Linq2d
{
    internal class ArrayQuery<T, R>: ArrayQuery1<R>, IArrayQuery<T, R>
    {
        public ArraySource<T> Source { get; }

        private Func<T[,], R[,]> _transform;

        public ArrayQuery(ArraySource<T> source, Expression<Func<Cell<T>, R>> kernel) : base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }

        public ArrayQuery(IArraySource<T> source, LambdaExpression kernel): base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source.Source;
        }

        public ArrayQuery(ArraySource<T> source, object resultInit, LambdaExpression kernel): base(source, kernel, resultInit)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(3).GetMethod().Name;
            Source = source;
        }

        public Func<T[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source.Array);
    }

    public interface IArrayQueryRecurrentHalf { }
    public interface IArrayQueryRecurrentHalf<T, R>: IArrayQueryRecurrentHalf, IArraySource<T> {}

    public interface IArrayQueryRecurrent<T, R, A>: IArrayQueryRecurrentHalf<T, R> {}

    internal class ArrayQueryRecurrent<T, R, A>: ArrayQuery<T, A>, IArrayQueryRecurrent<T, R, A>
    {
        public ArrayQueryRecurrent(ArraySource<T> source, Result<R> result, Expression<Func<Cell<T>, RelativeCell<R>, A>> kernel): base(source, result.InitValue, kernel){}
    }
    
    internal class ArrayQuery<T1, T2, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, R>
    {
        public ArrayQuery(ArraySource<T1> source1, ArraySource<T2> source2, Expression<Func<Cell<T1>, Cell<T2>, R>> kernel) : base(source1, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source1 ?? throw new ArgumentNullException(nameof(source1));
            Source2 = source2 ?? throw new ArgumentNullException(nameof(source2));
        }
        public ArrayQuery(IArraySource<T1, T2> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }

        public ArrayQuery(IArraySource<T1, T2> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }

        public ArraySource<T1> Source1 { get; }
        public ArraySource<T2> Source2 { get; }
        private Func<T1[,], T2[,], R[,]> _transform;
        public Func<T1[,], T2[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array);

    }
    

    internal class ArrayQuery<T1, T2, T3, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }

        private Func<T1[,], T2[,], T3[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2> sources, ArraySource<T3> source3, LambdaExpression kernel) : base(sources, source3, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = source3 ?? throw new ArgumentNullException(nameof(source3));
        }

        public ArrayQuery(IArraySource<T1, T2, T3> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }

        public ArrayQuery(IArraySource<T1, T2, T3> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }

        public Func<T1[,], T2[,], T3[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3> sources, ArraySource<T4> source4, LambdaExpression kernel) : base(sources, source4, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = source4 ?? throw new ArgumentNullException(nameof(source4));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, T5, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, T5, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3, T4> sources, ArraySource<T5> source5, LambdaExpression kernel) : base(sources, source5, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = source5 ?? throw new ArgumentNullException(nameof(source5));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5> sources, ArraySource<T6> source6, LambdaExpression kernel) : base(sources, source6, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = source6 ?? throw new ArgumentNullException(nameof(source6));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, T7, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, T7, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6> sources, ArraySource<T7> source7, LambdaExpression kernel) : base(sources, source7, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = source7 ?? throw new ArgumentNullException(nameof(source7));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }
        public ArraySource<T8> Source8{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, ArraySource<T8> source8, LambdaExpression kernel) : base(sources, source8, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = source8 ?? throw new ArgumentNullException(nameof(source8));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array, Source8.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }
        public ArraySource<T8> Source8{ get; }
        public ArraySource<T9> Source9{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8> sources, ArraySource<T9> source9, LambdaExpression kernel) : base(sources, source9, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = source9 ?? throw new ArgumentNullException(nameof(source9));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array, Source8.Array, Source9.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }
        public ArraySource<T8> Source8{ get; }
        public ArraySource<T9> Source9{ get; }
        public ArraySource<T10> Source10{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9> sources, ArraySource<T10> source10, LambdaExpression kernel) : base(sources, source10, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = source10 ?? throw new ArgumentNullException(nameof(source10));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array, Source8.Array, Source9.Array, Source10.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }
        public ArraySource<T8> Source8{ get; }
        public ArraySource<T9> Source9{ get; }
        public ArraySource<T10> Source10{ get; }
        public ArraySource<T11> Source11{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> sources, ArraySource<T11> source11, LambdaExpression kernel) : base(sources, source11, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = source11 ?? throw new ArgumentNullException(nameof(source11));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array, Source8.Array, Source9.Array, Source10.Array, Source11.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }
        public ArraySource<T8> Source8{ get; }
        public ArraySource<T9> Source9{ get; }
        public ArraySource<T10> Source10{ get; }
        public ArraySource<T11> Source11{ get; }
        public ArraySource<T12> Source12{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> sources, ArraySource<T12> source12, LambdaExpression kernel) : base(sources, source12, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = source12 ?? throw new ArgumentNullException(nameof(source12));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array, Source8.Array, Source9.Array, Source10.Array, Source11.Array, Source12.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }
        public ArraySource<T8> Source8{ get; }
        public ArraySource<T9> Source9{ get; }
        public ArraySource<T10> Source10{ get; }
        public ArraySource<T11> Source11{ get; }
        public ArraySource<T12> Source12{ get; }
        public ArraySource<T13> Source13{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> sources, ArraySource<T13> source13, LambdaExpression kernel) : base(sources, source13, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
            Source13 = source13 ?? throw new ArgumentNullException(nameof(source13));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
            Source13 = sources.Source13;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
            Source13 = sources.Source13;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array, Source8.Array, Source9.Array, Source10.Array, Source11.Array, Source12.Array, Source13.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }
        public ArraySource<T8> Source8{ get; }
        public ArraySource<T9> Source9{ get; }
        public ArraySource<T10> Source10{ get; }
        public ArraySource<T11> Source11{ get; }
        public ArraySource<T12> Source12{ get; }
        public ArraySource<T13> Source13{ get; }
        public ArraySource<T14> Source14{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> sources, ArraySource<T14> source14, LambdaExpression kernel) : base(sources, source14, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
            Source13 = sources.Source13;
            Source14 = source14 ?? throw new ArgumentNullException(nameof(source14));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
            Source13 = sources.Source13;
            Source14 = sources.Source14;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
            Source13 = sources.Source13;
            Source14 = sources.Source14;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array, Source8.Array, Source9.Array, Source10.Array, Source11.Array, Source12.Array, Source13.Array, Source14.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }
        public ArraySource<T8> Source8{ get; }
        public ArraySource<T9> Source9{ get; }
        public ArraySource<T10> Source10{ get; }
        public ArraySource<T11> Source11{ get; }
        public ArraySource<T12> Source12{ get; }
        public ArraySource<T13> Source13{ get; }
        public ArraySource<T14> Source14{ get; }
        public ArraySource<T15> Source15{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> sources, ArraySource<T15> source15, LambdaExpression kernel) : base(sources, source15, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
            Source13 = sources.Source13;
            Source14 = sources.Source14;
            Source15 = source15 ?? throw new ArgumentNullException(nameof(source15));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
            Source13 = sources.Source13;
            Source14 = sources.Source14;
            Source15 = sources.Source15;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
            Source13 = sources.Source13;
            Source14 = sources.Source14;
            Source15 = sources.Source15;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array, Source8.Array, Source9.Array, Source10.Array, Source11.Array, Source12.Array, Source13.Array, Source14.Array, Source15.Array);
    }

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R> : ArrayQuery1<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }
        public ArraySource<T8> Source8{ get; }
        public ArraySource<T9> Source9{ get; }
        public ArraySource<T10> Source10{ get; }
        public ArraySource<T11> Source11{ get; }
        public ArraySource<T12> Source12{ get; }
        public ArraySource<T13> Source13{ get; }
        public ArraySource<T14> Source14{ get; }
        public ArraySource<T15> Source15{ get; }
        public ArraySource<T16> Source16{ get; }

        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], T16[,], R[,]> _transform;

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> sources, ArraySource<T16> source16, LambdaExpression kernel) : base(sources, source16, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
            Source13 = sources.Source13;
            Source14 = sources.Source14;
            Source15 = sources.Source15;
            Source16 = source16 ?? throw new ArgumentNullException(nameof(source16));
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> sources, Result<R> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
            Source13 = sources.Source13;
            Source14 = sources.Source14;
            Source15 = sources.Source15;
            Source16 = sources.Source16;
        }

        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = sources.Source7;
            Source8 = sources.Source8;
            Source9 = sources.Source9;
            Source10 = sources.Source10;
            Source11 = sources.Source11;
            Source12 = sources.Source12;
            Source13 = sources.Source13;
            Source14 = sources.Source14;
            Source15 = sources.Source15;
            Source16 = sources.Source16;
        }

        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], T16[,], R[,]> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], T16[,], R[,]>>();
                return _transform;
            }
        }

        protected override R[,] GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array, Source8.Array, Source9.Array, Source10.Array, Source11.Array, Source12.Array, Source13.Array, Source14.Array, Source15.Array, Source16.Array);
    }

    internal class ArrayQuery2<T, R1, R2>: ArrayQuery2<R1, R2>, IArrayQuery2<T, R1, R2>
    {
        public ArraySource<T> Source { get; }

        private Func<T[,], (R1[,], R2[,])> _transform;


        public ArrayQuery2(ArraySource<T> source, Expression<Func<Cell<T>, (R1, R2)>> kernel) : base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }
        public ArrayQuery2(ArraySource<T> source, Result<R1> result, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2)>> kernel):base(source, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }

        public ArrayQuery2(IArraySource<T> source, LambdaExpression kernel): base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source.Source;
        }

        public ArrayQuery2(IArrayQueryRecurrentHalf<T, R1> source, Result<R2> result, LambdaExpression kernel): base(source, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source.Source;
        }


        public Func<T[,], (R1[,], R2[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T[,], (R1[,], R2[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,]) GetResult() => Transform(Source.Array);

    }

    internal class ArrayQuery2<T1, T2, R1, R2> : ArrayQuery2<R1, R2>, IArrayQuery2<T1, T2, R1, R2>
    {
        public ArrayQuery2(ArraySource<T1> source1, ArraySource<T2> source2, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> kernel) : base(source1, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source1 ?? throw new ArgumentNullException(nameof(source1));
            Source2 = source2 ?? throw new ArgumentNullException(nameof(source2));
        }
        public ArrayQuery2(IArraySource<T1, T2> sources, Result<R1> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArraySource<T1> Source1 { get; }
        public ArraySource<T2> Source2 { get; }
        private Func<T1[,], T2[,], (R1[,], R2[,])> _transform;
        public Func<T1[,], T2[,], (R1[,], R2[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], (R1[,], R2[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,]) GetResult() => Transform(Source1.Array, Source2.Array);

    }

    # region 3 results
    internal class ArrayQuery3<T, R1, R2, R3>: ArrayQuery3<R1, R2, R3>, IArrayQuery3<T, R1, R2, R3>
    {
        public ArraySource<T> Source { get; }

        private Func<T[,], (R1[,], R2[,], R3[,])> _transform;


        public ArrayQuery3(ArraySource<T> source, Expression<Func<Cell<T>, (R1, R2, R3)>> kernel) : base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }
        public ArrayQuery3(ArraySource<T> source, Result<R1> result, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3)>> kernel):base(source, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }

        public ArrayQuery3(IArraySource<T> source, LambdaExpression kernel): base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source.Source;
        }

        public ArrayQuery3(IArrayQueryRecurrentHalf<T, R1> source, Result<R2> result, LambdaExpression kernel): base(source, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source.Source;
        }


        public Func<T[,], (R1[,], R2[,], R3[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T[,], (R1[,], R2[,], R3[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,]) GetResult() => Transform(Source.Array);

    }

    internal class ArrayQuery3<T1, T2, R1, R2, R3> : ArrayQuery3<R1, R2, R3>, IArrayQuery3<T1, T2, R1, R2, R3>
    {
        public ArrayQuery3(ArraySource<T1> source1, ArraySource<T2> source2, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2, R3)>> kernel) : base(source1, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source1 ?? throw new ArgumentNullException(nameof(source1));
            Source2 = source2 ?? throw new ArgumentNullException(nameof(source2));
        }
        public ArrayQuery3(IArraySource<T1, T2> sources, Result<R1> result, LambdaExpression kernel) : base(sources, kernel, result.InitValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArraySource<T1> Source1 { get; }
        public ArraySource<T2> Source2 { get; }
        private Func<T1[,], T2[,], (R1[,], R2[,], R3[,])> _transform;

        public Func<T1[,], T2[,], (R1[,], R2[,], R3[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], (R1[,], R2[,], R3[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,]) GetResult() => Transform(Source1.Array, Source2.Array);

    }

    # endregion
}