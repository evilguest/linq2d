using System;
using System.Collections.Generic;
using System.Reflection;

using System.Runtime.Intrinsics;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace Linq2d.CodeGen
{

    public class VectorData
    {
        private static Dictionary<int, IVectorInfo> _vectorInfo = new Dictionary<int, IVectorInfo>()
        {
            [2] = new Vector2Info(),
            [4] = new Vector4Info(),
            [8] = new Vector8Info(),
            [16] = new Vector16Info(),
            [32] = new Vector32Info()
        };

        public static IReadOnlyDictionary<int, IVectorInfo> VectorInfo { get => _vectorInfo; }

        public static IReadOnlyDictionary<MethodInfo, MethodInfo> MethodTable { get; } = new Dictionary<MethodInfo, MethodInfo>();



        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector256<long> Negate(Vector256<long> a) => Avx2.MultiplyLow(a, Vector256.Create(-1L));



        [ExcludeFromCodeCoverage]
        public static R Load<T, R>(T[,] array, int i, int j, int size)
            => throw new NotImplementedException();
    }
}
