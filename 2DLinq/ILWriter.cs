using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Processing2d
{
    public static class ILWriter
    {
        private static Label DefineAndMarkLabel(this ILGenerator target)
        {
            var result = target.DefineLabel();
            target.MarkLabel(result);
            return result;
        }

        private static void EmitCSharpFor(this ILGenerator target, Action check, Action bodyAndStep)
        {
            var entryPoint = target.DefineLabel();

            target.Emit(OpCodes.Br, entryPoint);

            var loopStart = target.DefineAndMarkLabel();

            bodyAndStep();

            target.MarkLabel(entryPoint);
            check();
            target.Emit(OpCodes.Brtrue, loopStart);

        }

        private static void EmitStraightFor(this ILGenerator target, Action check, Action bodyAndStep, int unrollFactor)
        {

            var loopStart = target.DefineAndMarkLabel();
            var loopEnd = target.DefineLabel();

            for (var i = 0; i < unrollFactor; i++)
            {
                check();
                target.Emit(OpCodes.Brfalse, loopEnd);
                bodyAndStep();
            }

            target.Emit(OpCodes.Br, loopStart);

            target.MarkLabel(loopEnd);
        }

        private static void EmitFor(this ILGenerator target, Action check, Action bodyAndStep, int unrollFactor) => EmitStraightFor(target, check, bodyAndStep, unrollFactor);

        public static void EmitFor(this ILGenerator target, LocalBuilder indexVar, int start, LocalBuilder limitVar, Action body, int unrollFactor = 1)
        {
            target.Emit(OpCodes.Ldc_I4, start);
            target.Emit(OpCodes.Stloc, indexVar); // i = start
            target.EmitFor(
                check: () =>
                {
                    target.Emit(OpCodes.Ldloc, indexVar);
                    target.Emit(OpCodes.Ldloc, limitVar);
                    target.Emit(OpCodes.Clt); // i<limit
                }, bodyAndStep: () =>
                {
                    body();
                    target.EmitIncrease(indexVar, 1); // i++;
                }, unrollFactor);
        }

        public static void EmitFor(this ILGenerator target, LocalBuilder indexVar, LocalBuilder start, LocalBuilder limitVar, Action body, int unrollFactor = 1)
        {
            target.Emit(OpCodes.Ldloc, start);
            target.Emit(OpCodes.Stloc, indexVar); // i = start
            target.EmitFor(
                check: () =>
                {
                    target.Emit(OpCodes.Ldloc, indexVar);
                    target.Emit(OpCodes.Ldloc, limitVar);
                    target.Emit(OpCodes.Clt); // i<limit
                }, bodyAndStep: () =>
                {
                    body();
                    target.EmitIncrease(indexVar, 1); // i++;
                }, unrollFactor);
        }

        public static void EmitFor(this ILGenerator target, LocalBuilder indexVar, int start, int limit, Action body, int unrollFactor = 1)
        {
            target.Emit(OpCodes.Ldc_I4, start);
            target.Emit(OpCodes.Stloc, indexVar); // i = low;
            target.EmitFor(check: () =>
            {
                target.Emit(OpCodes.Ldloc, indexVar);
                target.Emit(OpCodes.Ldc_I4, limit);
                target.Emit(OpCodes.Clt); // i<limit 
            }, bodyAndStep:() =>
            {
                body();
                target.EmitIncrease(indexVar, 1); // i++;
            }, unrollFactor);
        }

        public static void EmitDebug<T>(this ILGenerator target, string label = null)
        {
            if(!string.IsNullOrEmpty(label))
            {
                target.Emit(OpCodes.Ldstr, label+" :");
                target.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new[] { typeof(string) }));
            }

            target.Emit(OpCodes.Dup);

            var meth = typeof(Console).GetMethod("WriteLine", new[] { typeof(T) });

            if (typeof(T).IsValueType && meth.GetParameters()[0].ParameterType == typeof(object))
                target.Emit(OpCodes.Box, typeof(T));

            target.Emit(OpCodes.Call, meth);
        }

        public static void EmitIncrease(this ILGenerator target, LocalBuilder variable, int operand)
        {
            if (operand == 0)
                return; // simple optimization

            target.Emit(OpCodes.Ldloc, variable); 
            target.Emit(OpCodes.Ldc_I4, operand);
            target.Emit(OpCodes.Add);
            target.Emit(OpCodes.Stloc, variable); // variable += operand

        }
    }
}
