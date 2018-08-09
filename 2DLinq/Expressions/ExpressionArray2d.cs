using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace System.Linq.Processing2d
{
    internal class ExpressionArray2d
    {
        public static Filter<T, R> GenerateExprFilter<T, R>(Expression<Kernel<T, R>> kernelExpr)
            where R : unmanaged
        {
            var km = KernelMeasure.Measure(kernelExpr.Compile());
            var data_arg = Parameter(typeof(T[,]), "data");
            var result_var = Variable(typeof(R[,]), "result");
            var i_var = Variable(typeof(int), "i");
            var j_var = Variable(typeof(int), "j");
            var h_var = Variable(typeof(int), "h");
            var w_var = Variable(typeof(int), "w");
            var fe = Block(new[] { data_arg, h_var, w_var, result_var },
                Assign(h_var, Call(data_arg, typeof(T[,]).GetMethod("GetLength", new [] { typeof(int) }), Constant(0))),
                Assign(w_var, Call(data_arg, typeof(T[,]).GetMethod("GetLength", new [] { typeof(int) }), Constant(1))),
                Assign(result_var, New(typeof(R[,]).GetConstructor(new[] { typeof(int), typeof(int) }), h_var, w_var)),
                ExpressionHelper.For(i_var,
                    Constant(-km.xmin),
                    LessThan(i_var, Subtract(h_var, Constant(km.xmax))),
                    AddAssign(i_var, Constant(1)),
                        ExpressionHelper.For(j_var,
                        Constant(-km.ymin),
                        LessThan(j_var, Subtract(h_var, Constant(km.xmax))),
                        AddAssign(j_var, Constant(1)),
                        Assign(MakeIndex(result_var, typeof(R[,]).GetProperty("Item", new Type[] { typeof(int), typeof(int) }), new[] { i_var, j_var }),
                            kernelExpr))
                    )
               );
            var nne = Lambda<Filter<T, R>>(fe, data_arg);
            var ff = nne.Compile();
            return ff;
        }
    }
}
