using System;
using System.Linq.Expressions;

namespace Linq2d
{
    internal class ArrayQuery<T, R>: ArrayQuery<R>, IArrayQuery<T, R>
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

        public ArrayQuery(ArraySource<T> source, R resultInit, LambdaExpression kernel): base(source, kernel, resultInit)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(3).GetMethod().Name;
            Source = source;
        }

        public ArrayQuery(IArraySource<T> source, R resultInit, LambdaExpression kernel): base(source.Source, kernel, resultInit)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(3).GetMethod().Name;
            Source = source.Source;
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

    internal class ArrayQueryRecurrent<T, R, A>: ArrayQuery<T, R>, IArrayQueryRecurrent<T, R, A>
    {
        public ArrayQueryRecurrent(ArraySource<T> source, R initValue, LambdaExpression kernel): base(source, initValue, kernel){}
    }
    
    #region one result
    internal class ArrayQuery<T1, T2, R> : ArrayQuery<R>, IArrayQuery<T1, T2, R>
    {
        public ArrayQuery(ArraySource<T1> source1, ArraySource<T2> source2, Expression<Func<Cell<T1>, Cell<T2>, R>> kernel) : base(source1, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source1 ?? throw new ArgumentNullException(nameof(source1));
            Source2 = source2 ?? throw new ArgumentNullException(nameof(source2));
        }

        public ArrayQuery(IArraySource<T1> source, ArraySource<T2> source2, LambdaExpression kernel) : base(source.Source, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source.Source;
            Source2 = source2 ?? throw new ArgumentNullException(nameof(source2));
        }

        public ArrayQuery(IArraySource<T1, T2> sources, R initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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
    

    internal class ArrayQuery<T1, T2, T3, R> : ArrayQuery<R>, IArrayQuery<T1, T2, T3, R>
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

        public ArrayQuery(IArraySource<T1, T2, T3> sources, R initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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

    internal class ArrayQuery<T1, T2, T3, T4, R> : ArrayQuery<R>, IArrayQuery<T1, T2, T3, T4, R>
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

        public ArrayQuery(IArraySource<T1, T2, T3, T4> sources, R initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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
    #endregion

    #region 2 results
    internal class ArrayQuery2<T, R1, R2>: ArrayQuery2<R1, R2>, IArrayQuery2<T, R1, R2>
    {
        public ArraySource<T> Source { get; }

        private Func<T[,], (R1[,], R2[,])> _transform;


        public ArrayQuery2(ArraySource<T> source, Expression<Func<Cell<T>, (R1, R2)>> kernel) : base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }
        public ArrayQuery2(ArraySource<T> source, R1 initValue, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2)>> kernel):base(source, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }

        public ArrayQuery2(IArraySource<T> source, LambdaExpression kernel): base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source.Source;
        }

        public ArrayQuery2(IArraySource<T> source, R1 initValue, LambdaExpression kernel):base(source, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source.Source;
        }

        public ArrayQuery2(IArrayQueryRecurrentHalf<T, R1> source, R2 initValue, LambdaExpression kernel): base(source, kernel, initValue)
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
        public ArrayQuery2(IArraySource<T1> source, ArraySource<T2> source2, LambdaExpression kernel) : base(source.Source, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source.Source;
            Source2 = source2 ?? throw new ArgumentNullException(nameof(source2));
        }
        public ArrayQuery2(IArraySource<T1, T2> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArrayQuery2(IArraySource<T1, T2> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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

    internal class ArrayQuery2<T1, T2, T3, R1, R2> : ArrayQuery2<R1, R2>, IArrayQuery2<T1, T2, T3, R1, R2>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArrayQuery2(IArraySource<T1, T2> sources, ArraySource<T3> source3, LambdaExpression kernel) : base(sources, source3, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = source3;
        }
        public ArrayQuery2(IArraySource<T1, T2> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArrayQuery2(IArraySource<T1, T2> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        private Func<T1[,], T2[,], T3[,], (R1[,], R2[,])> _transform;
        public Func<T1[,], T2[,], T3[,], (R1[,], R2[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], (R1[,], R2[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array);

    }
    internal class ArrayQuery2<T1, T2, T3, T4, R1, R2> : ArrayQuery2<R1, R2>, IArrayQuery2<T1, T2, T3, T4, R1, R2>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArrayQuery2(IArraySource<T1, T2, T3> sources, ArraySource<T4> source4, LambdaExpression kernel) : base(sources, source4, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = source4;
        }
        public ArrayQuery2(IArraySource<T1, T2, T3> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }
        public ArrayQuery2(IArraySource<T1, T2, T3> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }
        private Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,])> _transform;
        public Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array);

    }
    #endregion
    #region 3 results
    internal class ArrayQuery3<T, R1, R2, R3>: ArrayQuery3<R1, R2, R3>, IArrayQuery3<T, R1, R2, R3>
    {
        public ArraySource<T> Source { get; }

        private Func<T[,], (R1[,], R2[,], R3[,])> _transform;


        public ArrayQuery3(ArraySource<T> source, Expression<Func<Cell<T>, (R1, R2, R3)>> kernel) : base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }
        public ArrayQuery3(ArraySource<T> source, R1 initValue, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3)>> kernel):base(source, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }

        public ArrayQuery3(IArraySource<T> source, LambdaExpression kernel): base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source.Source;
        }

        public ArrayQuery3(IArraySource<T> source, R1 initValue, LambdaExpression kernel):base(source, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source.Source;
        }

        public ArrayQuery3(IArrayQueryRecurrentHalf<T, R1> source, R2 initValue, LambdaExpression kernel): base(source, kernel, initValue)
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
        public ArrayQuery3(IArraySource<T1> source, ArraySource<T2> source2, LambdaExpression kernel) : base(source.Source, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source.Source;
            Source2 = source2 ?? throw new ArgumentNullException(nameof(source2));
        }
        public ArrayQuery3(IArraySource<T1, T2> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArrayQuery3(IArraySource<T1, T2> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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

    internal class ArrayQuery3<T1, T2, T3, R1, R2, R3> : ArrayQuery3<R1, R2, R3>, IArrayQuery3<T1, T2, T3, R1, R2, R3>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArrayQuery3(IArraySource<T1, T2> sources, ArraySource<T3> source3, LambdaExpression kernel) : base(sources, source3, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = source3;
        }
        public ArrayQuery3(IArraySource<T1, T2> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArrayQuery3(IArraySource<T1, T2> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        private Func<T1[,], T2[,], T3[,], (R1[,], R2[,], R3[,])> _transform;
        public Func<T1[,], T2[,], T3[,], (R1[,], R2[,], R3[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], (R1[,], R2[,], R3[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array);

    }
    internal class ArrayQuery3<T1, T2, T3, T4, R1, R2, R3> : ArrayQuery3<R1, R2, R3>, IArrayQuery3<T1, T2, T3, T4, R1, R2, R3>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArrayQuery3(IArraySource<T1, T2, T3> sources, ArraySource<T4> source4, LambdaExpression kernel) : base(sources, source4, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = source4;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }
        private Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,], R3[,])> _transform;
        public Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,], R3[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,], R3[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array);

    }
    #endregion
    #region 4 results
    internal class ArrayQuery4<T, R1, R2, R3, R4>: ArrayQuery4<R1, R2, R3, R4>, IArrayQuery4<T, R1, R2, R3, R4>
    {
        public ArraySource<T> Source { get; }

        private Func<T[,], (R1[,], R2[,], R3[,], R4[,])> _transform;


        public ArrayQuery4(ArraySource<T> source, Expression<Func<Cell<T>, (R1, R2, R3, R4)>> kernel) : base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }
        public ArrayQuery4(ArraySource<T> source, R1 initValue, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3, R4)>> kernel):base(source, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }

        public ArrayQuery4(IArraySource<T> source, LambdaExpression kernel): base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source.Source;
        }

        public ArrayQuery4(IArraySource<T> source, R1 initValue, LambdaExpression kernel):base(source, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source.Source;
        }

        public ArrayQuery4(IArrayQueryRecurrentHalf<T, R1> source, R2 initValue, LambdaExpression kernel): base(source, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source.Source;
        }


        public Func<T[,], (R1[,], R2[,], R3[,], R4[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T[,], (R1[,], R2[,], R3[,], R4[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,], R4[,]) GetResult() => Transform(Source.Array);

    }

    internal class ArrayQuery4<T1, T2, R1, R2, R3, R4> : ArrayQuery4<R1, R2, R3, R4>, IArrayQuery4<T1, T2, R1, R2, R3, R4>
    {
        public ArrayQuery4(ArraySource<T1> source1, ArraySource<T2> source2, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2, R3, R4)>> kernel) : base(source1, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source1 ?? throw new ArgumentNullException(nameof(source1));
            Source2 = source2 ?? throw new ArgumentNullException(nameof(source2));
        }
        public ArrayQuery4(IArraySource<T1> source, ArraySource<T2> source2, LambdaExpression kernel) : base(source.Source, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source.Source;
            Source2 = source2 ?? throw new ArgumentNullException(nameof(source2));
        }
        public ArrayQuery4(IArraySource<T1, T2> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArrayQuery4(IArraySource<T1, T2> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArraySource<T1> Source1 { get; }
        public ArraySource<T2> Source2 { get; }
        private Func<T1[,], T2[,], (R1[,], R2[,], R3[,], R4[,])> _transform;
        public Func<T1[,], T2[,], (R1[,], R2[,], R3[,], R4[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], (R1[,], R2[,], R3[,], R4[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,], R4[,]) GetResult() => Transform(Source1.Array, Source2.Array);

    }

    internal class ArrayQuery4<T1, T2, T3, R1, R2, R3, R4> : ArrayQuery4<R1, R2, R3, R4>, IArrayQuery4<T1, T2, T3, R1, R2, R3, R4>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArrayQuery4(IArraySource<T1, T2> sources, ArraySource<T3> source3, LambdaExpression kernel) : base(sources, source3, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = source3;
        }
        public ArrayQuery4(IArraySource<T1, T2> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArrayQuery4(IArraySource<T1, T2> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        private Func<T1[,], T2[,], T3[,], (R1[,], R2[,], R3[,], R4[,])> _transform;
        public Func<T1[,], T2[,], T3[,], (R1[,], R2[,], R3[,], R4[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], (R1[,], R2[,], R3[,], R4[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,], R4[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array);

    }
    internal class ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> : ArrayQuery4<R1, R2, R3, R4>, IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArrayQuery4(IArraySource<T1, T2, T3> sources, ArraySource<T4> source4, LambdaExpression kernel) : base(sources, source4, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = source4;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }
        private Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,], R3[,], R4[,])> _transform;
        public Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,], R3[,], R4[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,], R3[,], R4[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,], R4[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array);

    }
    #endregion
}
