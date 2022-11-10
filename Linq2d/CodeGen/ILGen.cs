using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Linq2d.CodeGen
{
    internal static class ILGen
    {
        //public static void Ldarg0(this ILGenerator ilg) => ilg.Emit(OpCodes.Ldarg_0);
        public static void Ldarg(this ILGenerator ilg, int arg) => ilg.Emit(OpCodes.Ldarg, arg);
        public static void Ldloc(this ILGenerator ilg, LocalBuilder local) => ilg.Emit(OpCodes.Ldloc, local);
        public static void Stloc(this ILGenerator ilg, LocalBuilder local) => ilg.Emit(OpCodes.Stloc, local);
        public static void Call(this ILGenerator ilg, MethodInfo method) => ilg.Emit(OpCodes.Call, method);
        //public static void Call<T1, T2, R>(this ILGenerator ilg, Func<T1, T2, R> method) => ilg.Emit(OpCodes.Call, method.Method);
        //public static void Call<T>(this ILGenerator ilg, Expression<Action<T>> action)
        //    => ilg.Emit(OpCodes.Call, action.Body is MethodCallExpression mce ? mce.Method : throw new ArgumentException("Wrong action", nameof(action)));
        //public static void Call<T, R>(this ILGenerator ilg, Expression<Func<T, R>> function)
        //    => ilg.Emit(OpCodes.Call, function.Body is MethodCallExpression mce ? mce.Method : throw new ArgumentException("Wrong function", nameof(function)));
        //public static void Call<T1, T2, R>(this ILGenerator ilg, Expression<Func<T1, T2, R>> function)
        //    => ilg.Emit(OpCodes.Call, function.Body is MethodCallExpression mce ? mce.Method : throw new ArgumentException("Wrong function", nameof(function)));
        public static void Call<T1, T2, T3, R>(this ILGenerator ilg, Expression<Func<T1, T2, T3, R>> function)
            => ilg.Emit(OpCodes.Call, function.Body is MethodCallExpression mce ? mce.Method : throw new ArgumentException("Wrong function", nameof(function)));
        //public static void Call(this ILGenerator ilg, Expression<Action> action)
        //    => ilg.Emit(OpCodes.Call, action.Body is MethodCallExpression mce ? mce.Method : throw new ArgumentException("Wrong action", nameof(action)));

        public static void Ldfld(this ILGenerator ilg, FieldInfo fi)
            => ilg.Emit(OpCodes.Ldfld, fi);
        public static void Ldfld<T, R>(this ILGenerator ilg, Expression<Func<T, R>> function)
            => ilg.Emit(OpCodes.Ldfld, function.Body is MemberExpression me
                                       && me.Member is FieldInfo fi ? fi : throw new ArgumentException("Wrong function for the field access", nameof(function)));
        public static void Newobj<T>(this ILGenerator ilg, Expression<Func<T>> construct) => ilg.Emit(OpCodes.Newobj, construct.Body switch
        {
            //NewExpression ne => ne.Constructor,
            NewArrayExpression nae => nae.Type.GetConstructor((from e in nae.Expressions select e.Type).ToArray()),
            _ => throw new ArgumentException("Wrong construction", nameof(construct)),
        });

        public static LocalBuilder DeclareLocal<T>(this ILGenerator ilg) => ilg.DeclareLocal(typeof(T));

        public static void Br(this ILGenerator ilg, Label label) => ilg.Emit(OpCodes.Br, label);

        public static void Ldc0(this ILGenerator ilg) => ilg.Emit(OpCodes.Ldc_I4_0);
        public static void Ldc1(this ILGenerator ilg) => ilg.Emit(OpCodes.Ldc_I4_1);
        public static void Ldc(this ILGenerator ilg, int constant) => ilg.Emit(OpCodes.Ldc_I4, constant);
        //public static void Ldc(this ILGenerator ilg, long constant) => ilg.Emit(OpCodes.Ldc_I8, constant);

        public static void Increment(this ILGenerator ilg, LocalBuilder var, int constant = 1)
        {
            ilg.Ldloc(var);
            switch (constant)
            {
                case 1: ilg.Ldc1(); break;
                default: ilg.Ldc(constant); break;
            }

            ilg.Emit(OpCodes.Add);
            ilg.Stloc(var);
        }

        public static Label DefineAndMarkLabel(this ILGenerator ilg)
        {
            var r = ilg.DefineLabel();
            ilg.MarkLabel(r);
            return r;
        }

        //public static void BrTrue(this ILGenerator ilg, Label label) => ilg.Emit(OpCodes.Brtrue, label);

        public static void Mul(this ILGenerator ilg) => ilg.Emit(OpCodes.Mul);
        //public static void Div(this ILGenerator ilg) => ilg.Emit(OpCodes.Div);
        public static void Add(this ILGenerator ilg) => ilg.Emit(OpCodes.Add);
        public static void Sub(this ILGenerator ilg) => ilg.Emit(OpCodes.Sub);

        //public static void MulByTypeSize<T>(this ILGenerator ilg) => ilg.MulByTypeSize(typeof(T));
        public static void MulByTypeSize(this ILGenerator ilg, Type t)
        {
            var s = (int)typeof(Unsafe).GetMethod("SizeOf").MakeGenericMethod(t).Invoke(null, null); // Marshal.SizeOf(t);
            if (s > 1)
            {
                ilg.Emit(OpCodes.Conv_I);
                ilg.Ldc(s);
                ilg.Mul();
            }
        }
        //public static void Store<T>(this ILGenerator ilg) => Store(ilg, typeof(T));
        
        public static void Store(this ILGenerator ilg, Type t)
        {
            OpCode storeCode;
            if (StoreTable.TryGetValue(t, out storeCode))
                ilg.Emit(storeCode);
            else
                ilg.Emit(OpCodes.Stobj, t);
        }

        public static IReadOnlyDictionary<Type, OpCode> StoreTable { get; } = new Dictionary<Type, OpCode>()
        {
            { typeof(sbyte), OpCodes.Stind_I1},
            { typeof(short), OpCodes.Stind_I2},
            { typeof(int), OpCodes.Stind_I4 },
            { typeof(long), OpCodes.Stind_I8 },
            { typeof(float), OpCodes.Stind_R4 },
            { typeof(double), OpCodes.Stind_R8 },
        };

        public static void Ret(this ILGenerator ilg) => ilg.Emit(OpCodes.Ret);
        //public static void Load2dPointerOffset<T>(this ILGenerator ilg, LocalBuilder pTrg, LocalBuilder w, LocalBuilder i, LocalBuilder j)
        //    => ilg.Load2dPointerOffset(typeof(T), pTrg, w, i, j);
        //public static void Load2dPointerOffset<T>(this ILGenerator ilg, LocalBuilder pTrg, LocalBuilder w, int i, LocalBuilder j)
        //    => ilg.Load2dPointerOffset(typeof(T), pTrg, w, i, j);
        //public static void Load2dPointerOffset<T>(this ILGenerator ilg, LocalBuilder pTrg, LocalBuilder w, LocalBuilder i, int j)
        //    => ilg.Load2dPointerOffset(typeof(T), pTrg, w, i, j);
        //public static void Load2dPointerOffset<T>(this ILGenerator ilg, LocalBuilder pTrg, LocalBuilder w, int i, int j)
        //    => ilg.Load2dPointerOffset(typeof(T), pTrg, w, i, j);
        //public static void Load2dPointerOffset(this ILGenerator ilg, Type t, LocalBuilder pTrg, LocalBuilder w, LocalBuilder i, LocalBuilder j)
        //{
        //    ilg.Ldloc(pTrg);
        //    ilg.Ldloc(i);
        //    ilg.Ldloc(w);
        //    ilg.Mul();
        //    ilg.Ldloc(j);
        //    ilg.Add();
        //    ilg.MulByTypeSize(t);
        //    ilg.Add();
        //}
        //public static void Load2dPointerOffset(this ILGenerator ilg, Type t, LocalBuilder pTrg, LocalBuilder w, int i, LocalBuilder j)
        //{
        //    ilg.Ldloc(pTrg);
        //    ilg.Ldloc(j);
        //    if (i > 0)
        //    {
        //        ilg.Ldloc(w);
        //        if (i > 1)
        //        {
        //            ilg.Ldc(i); ilg.Mul();
        //        }
        //        ilg.Add();
        //    }
        //    ilg.MulByTypeSize(t);
        //    ilg.Add();
        //}
        //public static void Load2dPointerOffset(this ILGenerator ilg, Type t, LocalBuilder pTrg, LocalBuilder w, LocalBuilder i, int j)
        //{
        //    ilg.Ldloc(pTrg);
        //    ilg.Ldloc(i);
        //    ilg.Ldloc(w);
        //    ilg.Mul();
        //    if (j != 0)
        //    {
        //        ilg.Ldc(j);
        //        ilg.Add();
        //    }
        //    ilg.MulByTypeSize(t);
        //    ilg.Add();
        //}
        //public static void Load2dPointerOffset(this ILGenerator ilg, Type t, LocalBuilder pTrg, LocalBuilder w, int i, int j)
        //{
        //    ilg.Ldloc(pTrg);
        //    if (i != 0)
        //    {
        //        ilg.Ldloc(w);
        //        if (i != 1)
        //        {
        //            ilg.Ldc(i); ilg.Mul();
        //        }
        //    }
        //    if (j != 0)
        //    {
        //        ilg.Ldc(j);
        //        if (i != 0)
        //            ilg.Add();
        //    }
        //    if (i != 0 || j != 0)
        //    {
        //        ilg.MulByTypeSize(t);
        //        ilg.Add();
        //    }
        //}

    }
}
