using System.Reflection.Emit;

namespace System.Linq.Processing2d
{
    class UnsafeInliningILInstructionVisitorBase<T> : KernelInliningILInstructionVisitor<T>
        where T : unmanaged
    {
        private readonly LocalBuilder _w;
        private readonly LocalBuilder _dy;

        public UnsafeInliningILInstructionVisitorBase(ILGenerator target, LocalBuilder i, LocalBuilder j, LocalBuilder w) : base(target, i, j)
        {
            _w = w;
            _dy = target.DeclareLocal(typeof(int));
        }

        unsafe protected override void EmitSourceAccessCode()
        {
            //          Opcode                   Stack state (head left)
            // dy, dx, psourcerow, ...
            //Target.EmitDebug<int>("dy");
            Target.Emit(OpCodes.Stloc, _dy);    // dx, psourcerow
            //Target.EmitDebug<int>("dx");
            Target.Emit(OpCodes.Ldloc, _w);     // w, dx, psourcerow
            //Target.EmitDebug<int>("W");
            Target.Emit(OpCodes.Mul);           // w*dx, psourcerow, ...
            Target.Emit(OpCodes.Ldloc, _dy);    // dy, w*dx, psourcerow, ...
            Target.Emit(OpCodes.Add);           // dy+w*dx, psourcerow, ...
            Target.Emit(OpCodes.Ldc_I4, sizeof(T));// sizeofT, dy+w*dx, psourcerow, ...
            Target.Emit(OpCodes.Mul);           // sizeofT * (w*dy + dx), psourcerow, ...
            Target.Emit(OpCodes.Add);           // psourcerow + sizeofT * dy + sizeofT*w*dx, ...
            //Target.EmitDebug<int>("source");
            // TODO: replace with the correct type access
            Target.Emit(OpCodes.Ldind_I4);      // psourcerow[dy*w+dx] 

            //Target.EmitDebug<T>();
        }
    }

}