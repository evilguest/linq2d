using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Linq2d.CodeGen
{

    namespace Fake
    {
        public static class Vector32
        {
            public static Vector32<byte> AsByte<T>(this Vector32<T> v)
                where T: unmanaged => Unsafe.BitCast<int, Vector32<byte>>(v._i);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector32<byte> Create(byte b) => new Vector32<byte>(b, b, b, b);
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //public static Vector32<byte> CreateScalar(byte b) => new Vector32<byte>(b, 0, 0, 0);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector32<uint> Create(uint u) => new Vector32<uint>(u);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe void Store(byte* address, Vector32<byte> data) => *(int*)address = data._i;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe Vector32<byte> Load(byte* address) => new Vector32<byte>(address);

            public static Vector32<byte> ConvertToVector32Byte(Vector128<int> data)
            {
                var t = data.AsByte();
                return new Vector32<byte>(t.GetElement(0), t.GetElement(4), t.GetElement(8), t.GetElement(12));
            }
            public static Vector32<byte> ConvertToVector32Byte(Vector256<ulong> data)
            {
                var t = data.AsByte();
                return new Vector32<byte>(t.GetElement(0), t.GetElement(8), t.GetElement(16), t.GetElement(24));
            }

            public static Vector32<byte> DoubleConditional(Vector32<byte> ifTrue, Vector32<byte> ifFalse, Vector256<ulong> boolean)
            {
                int s = ConvertToVector32Byte(boolean)._i;
                return new Vector32<byte>((s & ifTrue._i) | (~s & ifFalse._i));
            }
        }

        public struct Vector32<T> where T : unmanaged
        {
            internal int _i;
            internal Vector32(byte b0, byte b1, byte b2, byte b3) =>
                _i = (b3 << 24) | (b2 << 16) | (b1 << 8) | b0;
            internal Vector32(int i) => _i = i;
            internal Vector32(uint u)
            {
                unchecked
                {
                    _i = (int)u;
                }
            }

            internal unsafe Vector32(T * address) => _i = *(int*)address;

        }
    }
}
