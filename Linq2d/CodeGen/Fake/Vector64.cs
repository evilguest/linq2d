using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Linq2d.CodeGen
{

    namespace Fake
    {
        public static class Vector64
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector64<byte> Create(byte b)
                => new Vector64<byte>(b, b, b, b, b, b, b, b);
            public static Vector64<int> Create(int i) => new Vector64<int>(i, i);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe void Store(int* address, Vector64<int> data) => *(ulong*)address = data._l;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe void Store(uint* address, Vector64<uint> data) => *(ulong*)address = data._l;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe void Store(sbyte* address, Vector64<sbyte> data) => *(ulong*)address = data._l;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe void Store(byte* address, Vector64<byte> data) => *(ulong*)address = data._l;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe Vector64<byte> Load(byte* address) => new Vector64<byte>(address);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe Vector64<sbyte> Load(sbyte* address) => new Vector64<sbyte>(address);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe Vector64<int> Load(int* address) => new Vector64<int>(address);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe Vector64<uint> Load(uint* address) => new Vector64<uint>(address);

            public static Vector32<byte> ConvertToVector32Byte(Vector128<int> data)
            {
                var t = data.AsByte();
                return new Vector32<byte>(t.GetElement(0), t.GetElement(4), t.GetElement(8), t.GetElement(12));
            }
            public static Vector32<byte> ConvertToVector32Byte(Vector256<long> data)
            {
                var t = data.AsByte();
                return new Vector32<byte>(t.GetElement(0), t.GetElement(8), t.GetElement(16), t.GetElement(24));
            }

            public static Vector32<byte> DoubleConditional(Vector32<byte> ifTrue, Vector32<byte> ifFalse, Vector256<byte> boolean)
            {
                int s = ConvertToVector32Byte(boolean.AsInt64())._i;
                return new Vector32<byte>((s & ifTrue._i) | (~s & ifFalse._i));
            }
        }

        public struct Vector64<T> where T : unmanaged
        {
            internal ulong _l;
            internal Vector64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7) =>
                _l = ((ulong)b7 << 56) | ((ulong)b6 << 48) | ((ulong)b5 << 40) | ((ulong)b4 << 32) | ((ulong)b3 << 24) | ((ulong)b2 << 16) | ((ulong)b1 << 8) | b0;
            //internal Vector64(short s0, short s1, short s2, short s3) =>
            //    _l =  ((long)s3 << 48) | (s2 << 32) | (s1 << 16) | s0;
            internal Vector64(int i0, int i1) =>
                _l = (((ulong)i1) << 32) | (ulong)i0;
            internal Vector64(uint i0, uint i1) =>
                _l = ((ulong)i1 << 32) | i0;

            //internal void Init(short s0, short s1) =>
            //    _i = ((ushort)s1 << 16) | (ushort)s0;
            //internal void Init(short s)
            //    => Init(s, s);
            //internal void InitScalar(short s)
            //    => _i = s;
            internal Vector64(ulong l) => _l = l;
            public unsafe Vector64(T *address) => _l = *(ulong*)address;
        }
    }
}
