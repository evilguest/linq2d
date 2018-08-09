using ClrTest.Reflection;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Processing2d
{
    internal abstract class KernelInliningILInstructionVisitor<T> : InliningILInstructionVisitor
    {
        protected LocalBuilder dy, i, j;
        protected static readonly MethodInfo _ra_getter = typeof(ICell<T>).GetMethod("get_Item", new Type[] { typeof(int), typeof(int) });
        public KernelInliningILInstructionVisitor(ILGenerator target, LocalBuilder i, LocalBuilder j) : base(target)
        {
            dy = Target.DeclareLocal(typeof(int));
            this.i = i;
            this.j = j;
        }

        public override void VisitInlineMethodInstruction(InlineMethodInstruction inlineMethodInstruction)
        {
            if (inlineMethodInstruction.Method.MethodHandle == _ra_getter.MethodHandle)
            {
                EmitSourceAccessCode();
            }
            else
                base.VisitInlineMethodInstruction(inlineMethodInstruction);
        }
        protected abstract void EmitSourceAccessCode();
    }

}