using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Processing2d.Fast
{
    internal class SafeInliningILInstructionVisitor<T> : KernelInliningILInstructionVisitor<T>
    {
        public SafeInliningILInstructionVisitor(ILGenerator target, LocalBuilder i, LocalBuilder j) : base(target, i, j)
        {
        }

        private static readonly MethodInfo _arrGetter = typeof(IArray2d<T>).GetMethod("get_Item", new Type[] { typeof(int), typeof(int) });

        protected override void EmitSourceAccessCode()
        {
            Target.Emit(OpCodes.Stloc, dy);
            Target.Emit(OpCodes.Ldloc, i);
            Target.Emit(OpCodes.Add);
            Target.Emit(OpCodes.Ldloc, j);
            Target.Emit(OpCodes.Ldloc, dy);
            Target.Emit(OpCodes.Add);
            Target.Emit(OpCodes.Callvirt, _arrGetter);
        }
    }

}