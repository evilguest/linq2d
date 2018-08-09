using ClrTest.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Processing2d
{
    unsafe class UnsafeInliningILInstructionVisitor<T> : UnsafeInliningILInstructionVisitorBase<T>
    where T : unmanaged
    {
        private readonly LocalBuilder _psource;
        public UnsafeInliningILInstructionVisitor(ILGenerator target, LocalBuilder i, LocalBuilder j, LocalBuilder w, LocalBuilder psource) : base(target, i, j, w)
        {
            _psource = psource;
        }

        public override void VisitInlineNoneInstruction(InlineNoneInstruction inlineNoneInstruction)
        {
            if (inlineNoneInstruction.OpCode == OpCodes.Ldarg_1)
                Target.Emit(OpCodes.Ldloc, _psource);
            else
                base.VisitInlineNoneInstruction(inlineNoneInstruction);
        }
        public override void VisitShortInlineIInstruction(ShortInlineIInstruction shortInlineIInstruction)
        {
            if (shortInlineIInstruction.OpCode == OpCodes.Ldarg_S && shortInlineIInstruction.Byte == 1)
                Target.Emit(OpCodes.Ldloc, _psource);
            else
                base.VisitShortInlineIInstruction(shortInlineIInstruction);
        }
        public override void VisitInlineIInstruction(InlineIInstruction inlineIInstruction)
        {
            if (inlineIInstruction.OpCode == OpCodes.Ldarg && inlineIInstruction.Int32 == 1)
                Target.Emit(OpCodes.Ldloc, _psource);
            else
                base.VisitInlineIInstruction(inlineIInstruction);
        }
    }

}