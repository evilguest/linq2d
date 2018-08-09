using ClrTest.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace System.Linq.Processing2d
{
    internal class InliningILInstructionVisitor : CopyingILInstructionVisitor
    {
        public InliningILInstructionVisitor(ILGenerator target) : base(target)
        {
        }

        public override void VisitInlineNoneInstruction(InlineNoneInstruction inlineNoneInstruction)
        {
            if (inlineNoneInstruction.OpCode != OpCodes.Ret)
                base.VisitInlineNoneInstruction(inlineNoneInstruction);
        }

    }

}