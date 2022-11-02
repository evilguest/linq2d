using System;
using System.Linq.Expressions;

namespace Linq2d
{
    #region 1 result

    internal class ArrayQuery<T, R> : ArrayQuery<R>, IArrayQuery<T, R>
    {
        public ArraySource<T> Source{ get; }
        public ArrayQuery(ArraySource<T> source, LambdaExpression kernel) : base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }
        public ArrayQuery(ArraySource<T> source, R initValue, LambdaExpression kernel) : base(source, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }



        public ArrayQuery(IArraySource<T> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        public ArrayQuery(IArraySource<T> sources, R initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        private Func<T[,], R[,]> _transform;
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

    internal class ArrayQuery<T1, T2, R> : ArrayQuery<R>, IArrayQuery<T1, T2, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }

        public ArrayQuery(ArraySource<T1> source1, ArraySource<T2> source2, LambdaExpression kernel) : base(source1, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source1;
            Source2 = source2;
        }

        public ArrayQuery(IArraySource<T1> sources, ArraySource<T2> source2, LambdaExpression kernel) : base(sources, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source;
            Source2 = source2;
        }
        public ArrayQuery(IArraySource<T1, T2> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArrayQuery(IArraySource<T1, T2> sources, R initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
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


        public ArrayQuery(IArraySource<T1, T2> sources, ArraySource<T3> source3, LambdaExpression kernel) : base(sources, source3, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = source3;
        }
        public ArrayQuery(IArraySource<T1, T2, T3> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }
        public ArrayQuery(IArraySource<T1, T2, T3> sources, R initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }
        private Func<T1[,], T2[,], T3[,], R[,]> _transform;
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


        public ArrayQuery(IArraySource<T1, T2, T3> sources, ArraySource<T4> source4, LambdaExpression kernel) : base(sources, source4, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = source4;
        }
        public ArrayQuery(IArraySource<T1, T2, T3, T4> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }
        public ArrayQuery(IArraySource<T1, T2, T3, T4> sources, R initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }
        private Func<T1[,], T2[,], T3[,], T4[,], R[,]> _transform;
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

    internal class ArrayQuery2<T, R1, R2> : ArrayQuery2<R1, R2>, IArrayQuery2<T, R1, R2>
    {
        public ArraySource<T> Source{ get; }
        public ArrayQuery2(ArraySource<T> source, LambdaExpression kernel) : base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }
        public ArrayQuery2(ArraySource<T> source, R1 initValue, LambdaExpression kernel) : base(source, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }



        public ArrayQuery2(IArraySource<T> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        public ArrayQuery2(IArraySource<T> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        public ArrayQuery2(IArraySource<T> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        private Func<T[,], (R1[,], R2[,])> _transform;
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
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }

        public ArrayQuery2(ArraySource<T1> source1, ArraySource<T2> source2, LambdaExpression kernel) : base(source1, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source1;
            Source2 = source2;
        }

        public ArrayQuery2(IArraySource<T1> sources, ArraySource<T2> source2, LambdaExpression kernel) : base(sources, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source;
            Source2 = source2;
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
        public ArrayQuery2(IArraySource<T1, T2> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
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
        public ArrayQuery2(IArraySource<T1, T2, T3> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
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
        public ArrayQuery2(IArraySource<T1, T2, T3, T4> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }
        public ArrayQuery2(IArraySource<T1, T2, T3, T4> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }
        public ArrayQuery2(IArraySource<T1, T2, T3, T4> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
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

    internal class ArrayQuery3<T, R1, R2, R3> : ArrayQuery3<R1, R2, R3>, IArrayQuery3<T, R1, R2, R3>
    {
        public ArraySource<T> Source{ get; }
        public ArrayQuery3(ArraySource<T> source, LambdaExpression kernel) : base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }
        public ArrayQuery3(ArraySource<T> source, R1 initValue, LambdaExpression kernel) : base(source, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }



        public ArrayQuery3(IArraySource<T> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        public ArrayQuery3(IArraySource<T> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        public ArrayQuery3(IArraySource<T> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        public ArrayQuery3(IArraySource<T> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        private Func<T[,], (R1[,], R2[,], R3[,])> _transform;
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
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }

        public ArrayQuery3(ArraySource<T1> source1, ArraySource<T2> source2, LambdaExpression kernel) : base(source1, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source1;
            Source2 = source2;
        }

        public ArrayQuery3(IArraySource<T1> sources, ArraySource<T2> source2, LambdaExpression kernel) : base(sources, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source;
            Source2 = source2;
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
        public ArrayQuery3(IArraySource<T1, T2> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArrayQuery3(IArraySource<T1, T2> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
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
        public ArrayQuery3(IArraySource<T1, T2, T3> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
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
        public ArrayQuery3(IArraySource<T1, T2, T3, T4> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3, T4> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3, T4> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3, T4> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
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
}
