using System;
using System.Linq.Expressions;
using Linq2d.Expressions;

namespace Linq2d
{
    #region 1 result

    internal abstract partial class ArrayQuery<R> : ArrayQueryBase
    {
        protected ArrayQuery(ArraySource source, LambdaExpression kernel) : base(source, kernel) { }
        protected ArrayQuery(IArrayQuery sources, LambdaExpression kernel) : base(sources, kernel) { }
        protected ArrayQuery(ArraySource left, ArraySource right, LambdaExpression kernel) : base(left, right, kernel) { }
        protected ArrayQuery(IArrayQuery sources, ArraySource right, LambdaExpression kernel) : base(sources, right, kernel) { }

        protected ArrayQuery(ArraySource source, LambdaExpression kernel, R initValue) : base(source, kernel, initValue) { }
        protected ArrayQuery(IArrayQuery sources, LambdaExpression kernel, R initValue) : base(sources, kernel, initValue) { }
        protected R[,] _result;
        protected abstract R[,] GetResult();
        public R[,] ToArray()
        {
            if (_result == null)
                _result = GetResult();
            return _result;
        }
    }


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
        public void Test(int a)
        {
            System.Console.WriteLine(a);

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

    internal class ArrayQuery<T1, T2, T3, T4, T5, R> : ArrayQuery<R>, IArrayQuery<T1, T2, T3, T4, T5, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }


        public ArrayQuery(IArraySource<T1, T2, T3, T4> sources, ArraySource<T5> source5, LambdaExpression kernel) : base(sources, source5, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = source5;
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
        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5> sources, R initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], R[,]> _transform;
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

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, R> : ArrayQuery<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }


        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5> sources, ArraySource<T6> source6, LambdaExpression kernel) : base(sources, source6, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = source6;
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
        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6> sources, R initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], R[,]> _transform;
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

    internal class ArrayQuery<T1, T2, T3, T4, T5, T6, T7, R> : ArrayQuery<R>, IArrayQuery<T1, T2, T3, T4, T5, T6, T7, R>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }


        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6> sources, ArraySource<T7> source7, LambdaExpression kernel) : base(sources, source7, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = source7;
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
        public ArrayQuery(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, R initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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
        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], R[,]> _transform;
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
    #endregion
    #region 2 results

    internal abstract partial class ArrayQuery2<R1, R2> : ArrayQueryBase
    {
        protected ArrayQuery2(ArraySource source, LambdaExpression kernel) : base(source, kernel) { }
        protected ArrayQuery2(IArrayQuery sources, LambdaExpression kernel) : base(sources, kernel) { }
        protected ArrayQuery2(ArraySource left, ArraySource right, LambdaExpression kernel) : base(left, right, kernel) { }
        protected ArrayQuery2(IArrayQuery sources, ArraySource right, LambdaExpression kernel) : base(sources, right, kernel) { }

        protected ArrayQuery2(ArraySource source, LambdaExpression kernel, R1 initValue1) : base(source, kernel, initValue1) { }
        protected ArrayQuery2(IArrayQuery sources, LambdaExpression kernel, R1 initValue1) : base(sources, kernel, initValue1) { }
        protected ArrayQuery2(IArrayQuery sources, LambdaExpression kernel, R2 initValue2) : base(sources, kernel, initValue2) { }
        protected (R1[,], R2[,])? _result;
        protected abstract (R1[,], R2[,]) GetResult();
        public (R1[,], R2[,]) ToArrays()
        {
            if (_result == null)
                _result = GetResult();
            return _result.Value;
        }
    }


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

    internal class ArrayQuery2<T1, T2, T3, T4, T5, R1, R2> : ArrayQuery2<R1, R2>, IArrayQuery2<T1, T2, T3, T4, T5, R1, R2>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }


        public ArrayQuery2(IArraySource<T1, T2, T3, T4> sources, ArraySource<T5> source5, LambdaExpression kernel) : base(sources, source5, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = source5;
        }
        public ArrayQuery2(IArraySource<T1, T2, T3, T4, T5> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        public ArrayQuery2(IArraySource<T1, T2, T3, T4, T5> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        public ArrayQuery2(IArraySource<T1, T2, T3, T4, T5> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,])> _transform;
        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array);

    }

    internal class ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2> : ArrayQuery2<R1, R2>, IArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }


        public ArrayQuery2(IArraySource<T1, T2, T3, T4, T5> sources, ArraySource<T6> source6, LambdaExpression kernel) : base(sources, source6, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = source6;
        }
        public ArrayQuery2(IArraySource<T1, T2, T3, T4, T5, T6> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        public ArrayQuery2(IArraySource<T1, T2, T3, T4, T5, T6> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        public ArrayQuery2(IArraySource<T1, T2, T3, T4, T5, T6> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,])> _transform;
        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array);

    }

    internal class ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2> : ArrayQuery2<R1, R2>, IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }


        public ArrayQuery2(IArraySource<T1, T2, T3, T4, T5, T6> sources, ArraySource<T7> source7, LambdaExpression kernel) : base(sources, source7, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = source7;
        }
        public ArrayQuery2(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, LambdaExpression kernel) : base(sources, kernel)
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
        public ArrayQuery2(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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
        public ArrayQuery2(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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
        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], (R1[,], R2[,])> _transform;
        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], (R1[,], R2[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], (R1[,], R2[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array);

    }
    #endregion
    #region 3 results

    internal abstract partial class ArrayQuery3<R1, R2, R3> : ArrayQueryBase
    {
        protected ArrayQuery3(ArraySource source, LambdaExpression kernel) : base(source, kernel) { }
        protected ArrayQuery3(IArrayQuery sources, LambdaExpression kernel) : base(sources, kernel) { }
        protected ArrayQuery3(ArraySource left, ArraySource right, LambdaExpression kernel) : base(left, right, kernel) { }
        protected ArrayQuery3(IArrayQuery sources, ArraySource right, LambdaExpression kernel) : base(sources, right, kernel) { }

        protected ArrayQuery3(ArraySource source, LambdaExpression kernel, R1 initValue1) : base(source, kernel, initValue1) { }
        protected ArrayQuery3(IArrayQuery sources, LambdaExpression kernel, R1 initValue1) : base(sources, kernel, initValue1) { }
        protected ArrayQuery3(IArrayQuery sources, LambdaExpression kernel, R2 initValue2) : base(sources, kernel, initValue2) { }
        protected ArrayQuery3(IArrayQuery sources, LambdaExpression kernel, R3 initValue3) : base(sources, kernel, initValue3) { }
        protected (R1[,], R2[,], R3[,])? _result;
        protected abstract (R1[,], R2[,], R3[,]) GetResult();
        public (R1[,], R2[,], R3[,]) ToArrays()
        {
            if (_result == null)
                _result = GetResult();
            return _result.Value;
        }
    }


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

    internal class ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3> : ArrayQuery3<R1, R2, R3>, IArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }


        public ArrayQuery3(IArraySource<T1, T2, T3, T4> sources, ArraySource<T5> source5, LambdaExpression kernel) : base(sources, source5, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = source5;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,], R3[,])> _transform;
        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,], R3[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,], R3[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array);

    }

    internal class ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3> : ArrayQuery3<R1, R2, R3>, IArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }


        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5> sources, ArraySource<T6> source6, LambdaExpression kernel) : base(sources, source6, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = source6;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5, T6> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5, T6> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5, T6> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5, T6> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,], R3[,])> _transform;
        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,], R3[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,], R3[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array);

    }

    internal class ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> : ArrayQuery3<R1, R2, R3>, IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }


        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5, T6> sources, ArraySource<T7> source7, LambdaExpression kernel) : base(sources, source7, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = source7;
        }
        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, LambdaExpression kernel) : base(sources, kernel)
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
        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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
        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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
        public ArrayQuery3(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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
        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], (R1[,], R2[,], R3[,])> _transform;
        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], (R1[,], R2[,], R3[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], (R1[,], R2[,], R3[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array);

    }
    #endregion
    #region 4 results

    internal abstract partial class ArrayQuery4<R1, R2, R3, R4> : ArrayQueryBase
    {
        protected ArrayQuery4(ArraySource source, LambdaExpression kernel) : base(source, kernel) { }
        protected ArrayQuery4(IArrayQuery sources, LambdaExpression kernel) : base(sources, kernel) { }
        protected ArrayQuery4(ArraySource left, ArraySource right, LambdaExpression kernel) : base(left, right, kernel) { }
        protected ArrayQuery4(IArrayQuery sources, ArraySource right, LambdaExpression kernel) : base(sources, right, kernel) { }

        protected ArrayQuery4(ArraySource source, LambdaExpression kernel, R1 initValue1) : base(source, kernel, initValue1) { }
        protected ArrayQuery4(IArrayQuery sources, LambdaExpression kernel, R1 initValue1) : base(sources, kernel, initValue1) { }
        protected ArrayQuery4(IArrayQuery sources, LambdaExpression kernel, R2 initValue2) : base(sources, kernel, initValue2) { }
        protected ArrayQuery4(IArrayQuery sources, LambdaExpression kernel, R3 initValue3) : base(sources, kernel, initValue3) { }
        protected ArrayQuery4(IArrayQuery sources, LambdaExpression kernel, R4 initValue4) : base(sources, kernel, initValue4) { }
        protected (R1[,], R2[,], R3[,], R4[,])? _result;
        protected abstract (R1[,], R2[,], R3[,], R4[,]) GetResult();
        public (R1[,], R2[,], R3[,], R4[,]) ToArrays()
        {
            if (_result == null)
                _result = GetResult();
            return _result.Value;
        }
    }


    internal class ArrayQuery4<T, R1, R2, R3, R4> : ArrayQuery4<R1, R2, R3, R4>, IArrayQuery4<T, R1, R2, R3, R4>
    {
        public ArraySource<T> Source{ get; }
        public ArrayQuery4(ArraySource<T> source, LambdaExpression kernel) : base(source, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }
        public ArrayQuery4(ArraySource<T> source, R1 initValue, LambdaExpression kernel) : base(source, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = source;
        }



        public ArrayQuery4(IArraySource<T> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        public ArrayQuery4(IArraySource<T> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        public ArrayQuery4(IArraySource<T> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        public ArrayQuery4(IArraySource<T> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        public ArrayQuery4(IArraySource<T> sources, R4 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source = sources.Source;
        }
        private Func<T[,], (R1[,], R2[,], R3[,], R4[,])> _transform;
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
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }

        public ArrayQuery4(ArraySource<T1> source1, ArraySource<T2> source2, LambdaExpression kernel) : base(source1, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = source1;
            Source2 = source2;
        }

        public ArrayQuery4(IArraySource<T1> sources, ArraySource<T2> source2, LambdaExpression kernel) : base(sources, source2, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source;
            Source2 = source2;
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
        public ArrayQuery4(IArraySource<T1, T2> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArrayQuery4(IArraySource<T1, T2> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
        public ArrayQuery4(IArraySource<T1, T2> sources, R4 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
        }
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
        public ArrayQuery4(IArraySource<T1, T2, T3> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3> sources, R4 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
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
        public ArrayQuery4(IArraySource<T1, T2, T3, T4> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4> sources, R4 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
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

    internal class ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4> : ArrayQuery4<R1, R2, R3, R4>, IArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }


        public ArrayQuery4(IArraySource<T1, T2, T3, T4> sources, ArraySource<T5> source5, LambdaExpression kernel) : base(sources, source5, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = source5;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5> sources, R4 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
        }
        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,], R3[,], R4[,])> _transform;
        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,], R3[,], R4[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,], R3[,], R4[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,], R4[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array);

    }

    internal class ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> : ArrayQuery4<R1, R2, R3, R4>, IArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }


        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5> sources, ArraySource<T6> source6, LambdaExpression kernel) : base(sources, source6, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = source6;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5, T6> sources, LambdaExpression kernel) : base(sources, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5, T6> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5, T6> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5, T6> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5, T6> sources, R4 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
        }
        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,], R3[,], R4[,])> _transform;
        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,], R3[,], R4[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,], R3[,], R4[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,], R4[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array);

    }

    internal class ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> : ArrayQuery4<R1, R2, R3, R4>, IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>
    {
        public ArraySource<T1> Source1{ get; }
        public ArraySource<T2> Source2{ get; }
        public ArraySource<T3> Source3{ get; }
        public ArraySource<T4> Source4{ get; }
        public ArraySource<T5> Source5{ get; }
        public ArraySource<T6> Source6{ get; }
        public ArraySource<T7> Source7{ get; }


        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5, T6> sources, ArraySource<T7> source7, LambdaExpression kernel) : base(sources, source7, kernel)
        {
            MethodName = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            Source1 = sources.Source1;
            Source2 = sources.Source2;
            Source3 = sources.Source3;
            Source4 = sources.Source4;
            Source5 = sources.Source5;
            Source6 = sources.Source6;
            Source7 = source7;
        }
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, LambdaExpression kernel) : base(sources, kernel)
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
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, R1 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, R2 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, R3 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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
        public ArrayQuery4(IArraySource<T1, T2, T3, T4, T5, T6, T7> sources, R4 initValue, LambdaExpression kernel) : base(sources, kernel, initValue)
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
        private Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], (R1[,], R2[,], R3[,], R4[,])> _transform;
        public Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], (R1[,], R2[,], R3[,], R4[,])> Transform
        {
            get
            {
                if (_transform == null)
                    _transform = BuildTransform<Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], (R1[,], R2[,], R3[,], R4[,])>>();
                return _transform;
            }
        }

        protected override (R1[,], R2[,], R3[,], R4[,]) GetResult() => Transform(Source1.Array, Source2.Array, Source3.Array, Source4.Array, Source5.Array, Source6.Array, Source7.Array);

    }
    #endregion
}
