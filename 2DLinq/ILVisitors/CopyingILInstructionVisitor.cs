using ClrTest.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace System.Linq.Processing2d
{
    internal class CopyingILInstructionVisitor : ILInstructionVisitor
    {
        private ILGenerator _target;
        protected ILGenerator Target {  get { return _target; } }
        private Dictionary<int, Label> _offsetLabels = new Dictionary<int, Label>();
        public void Walk(ILReader ilr)
        {
            foreach(var instruction in ilr)
            {
                MarkLabel(instruction);
                instruction.Accept(this);
            }
        }

        private Label GetLabelForOffset(int offset)
        {
            if (!_offsetLabels.ContainsKey(offset))
            {
                _offsetLabels[offset] = _target.DefineLabel();
            }
            return _offsetLabels[offset];
        }

        //private Label GetLabelForInstruction(ILInstruction instruction)
        //{
        //    return GetLabelForOffset(instruction.Offset);
        //}

        private void MarkLabel(ILInstruction instruction)
        {
            _target.MarkLabel(GetLabelForOffset(instruction.Offset));
        }

        public CopyingILInstructionVisitor(ILGenerator target)
        {
            _target = target ?? throw new ArgumentNullException("target");
        }

        public override void VisitInlineBrTargetInstruction(InlineBrTargetInstruction inlineBrTargetInstruction)
        {
            _target.Emit(inlineBrTargetInstruction.OpCode, GetLabelForOffset(inlineBrTargetInstruction.TargetOffset));
        }

        public override void VisitInlineFieldInstruction(InlineFieldInstruction inlineFieldInstruction)
        {
            _target.Emit(inlineFieldInstruction.OpCode, inlineFieldInstruction.Field);
        }

        public override void VisitInlineIInstruction(InlineIInstruction inlineIInstruction)
        {
            _target.Emit(inlineIInstruction.OpCode, inlineIInstruction.Int32);
        }

        public override void VisitInlineI8Instruction(InlineI8Instruction inlineI8Instruction)
        {
            _target.Emit(inlineI8Instruction.OpCode, inlineI8Instruction.Int64);
        }

        public override void VisitInlineMethodInstruction(InlineMethodInstruction inlineMethodInstruction)
        {
            _target.Emit(inlineMethodInstruction.OpCode, inlineMethodInstruction.Token);
        }

        public override void VisitInlineNoneInstruction(InlineNoneInstruction inlineNoneInstruction)
        {
            _target.Emit(inlineNoneInstruction.OpCode);
        }

        public override void VisitInlineRInstruction(InlineRInstruction inlineRInstruction)
        {
            _target.Emit(inlineRInstruction.OpCode, inlineRInstruction.Double);
        }

        public override void VisitInlineSigInstruction(InlineSigInstruction inlineSigInstruction)
        {
            _target.Emit(inlineSigInstruction.OpCode, inlineSigInstruction.Token);
        }

        public override void VisitInlineStringInstruction(InlineStringInstruction inlineStringInstruction)
        {
            _target.Emit(inlineStringInstruction.OpCode, inlineStringInstruction.String);
        }

        public override void VisitInlineSwitchInstruction(InlineSwitchInstruction inlineSwitchInstruction)
        {
            var labels = (from targetOffset in inlineSwitchInstruction.TargetOffsets select GetLabelForOffset(targetOffset)).ToArray();
            _target.Emit(inlineSwitchInstruction.OpCode, labels);
        }

        public override void VisitInlineTokInstruction(InlineTokInstruction inlineTokInstruction)
        {
            _target.Emit(inlineTokInstruction.OpCode, inlineTokInstruction.Token);
        }
        public override void VisitInlineTypeInstruction(InlineTypeInstruction inlineTypeInstruction)
        {
            _target.Emit(inlineTypeInstruction.OpCode, inlineTypeInstruction.Token);
        }
        public override void VisitInlineVarInstruction(InlineVarInstruction inlineVarInstruction)
        {
            _target.Emit(inlineVarInstruction.OpCode, inlineVarInstruction.Ordinal);
        }
        public override void VisitShortInlineBrTargetInstruction(ShortInlineBrTargetInstruction shortInlineBrTargetInstruction) {
            _target.Emit(shortInlineBrTargetInstruction.OpCode, GetLabelForOffset(shortInlineBrTargetInstruction.TargetOffset));
        }
        public override void VisitShortInlineIInstruction(ShortInlineIInstruction shortInlineIInstruction) {
            _target.Emit(shortInlineIInstruction.OpCode, shortInlineIInstruction.Byte);
        }
        public override void VisitShortInlineRInstruction(ShortInlineRInstruction shortInlineRInstruction) {
            _target.Emit(shortInlineRInstruction.OpCode, shortInlineRInstruction.Single);
        }
        public override void VisitShortInlineVarInstruction(ShortInlineVarInstruction shortInlineVarInstruction) {
            _target.Emit(shortInlineVarInstruction.OpCode, shortInlineVarInstruction.Ordinal);
        }

    }
}
