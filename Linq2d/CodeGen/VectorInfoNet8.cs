using Linq2d.CodeGen.Intrinsics;
using System.Linq.Expressions;
using System.Runtime.Intrinsics;
using System;

namespace Linq2d.CodeGen
{
    public abstract class VectorInfoNet8: VectorInfo
    {
        public VectorInfoNet8()
        {
            if (Avx512F.IsSupported)
            {
                InitAvx512F();
                Available = true;
            }
            if (Avx512F.VL.IsSupported)
            {
                InitAvx512FVL();
                Available = true;
            }
            if (Avx512BW.IsSupported)
            {
                InitAvx512BW();
                Available = true;
            }

        }
        public void InitBinary512<T>(ExpressionType ex, Func<Vector512<T>, Vector512<T>, Vector512<T>> method)
            where T : unmanaged
            => InitBinary(ex, method);

        #region overridables
        protected virtual void InitAvx512F() { }
        protected virtual void InitAvx512FVL() { }
        protected virtual void InitAvx512BW() { }
        #endregion
    }
}
