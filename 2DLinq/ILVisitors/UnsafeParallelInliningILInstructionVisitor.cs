using ClrTest.Reflection;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Processing2d
{
    unsafe internal class UnsafeParallelInliningILInstructionVisitor<T> : UnsafeInliningILInstructionVisitor<T>
        where T : unmanaged
    {
        private readonly FieldInfo _target;
        public UnsafeParallelInliningILInstructionVisitor(ILGenerator ilg, LocalBuilder tempI, LocalBuilder tempJ, LocalBuilder tempW, LocalBuilder srcPtr, FieldInfo target)
            : base(ilg, tempI, tempJ, tempW, srcPtr)
        {
            _target = target;
        }
        public override void VisitInlineNoneInstruction(InlineNoneInstruction inlineNoneInstruction)
        {
            base.VisitInlineNoneInstruction(inlineNoneInstruction);
            if (inlineNoneInstruction.OpCode == OpCodes.Ldarg_0)
                Target.Emit(OpCodes.Ldfld, _target);
        }
        public override void VisitShortInlineIInstruction(ShortInlineIInstruction shortInlineIInstruction)
        {
            base.VisitShortInlineIInstruction(shortInlineIInstruction);
            if (shortInlineIInstruction.OpCode == OpCodes.Ldarg_S && shortInlineIInstruction.Byte == 0)
                Target.Emit(OpCodes.Ldfld, _target);
        }
        public override void VisitInlineIInstruction(InlineIInstruction inlineIInstruction)
        {
            base.VisitInlineIInstruction(inlineIInstruction);
            if (inlineIInstruction.OpCode == OpCodes.Ldarg && inlineIInstruction.Int32 == 0)
                Target.Emit(OpCodes.Ldfld, _target);
        }
    }

}