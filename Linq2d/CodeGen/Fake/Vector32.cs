using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Linq2d.CodeGen
{

    namespace Fake
    {
        public static class Vector32
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector32<byte> Create(byte b)
                => new Vector32<byte>(b, b, b, b);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe void Store(byte* address, Vector32<byte> data)
                => *(int*)address = data._i;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe Vector32<byte> Load(byte* address)
            {
                var r = new Vector32<byte>();
                r._i = *(int*)address;
                return r;
            }

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

        public struct Vector32<T> where T : struct
        {
            //[FieldOffset(0)]
            //private byte _b0;
            //[FieldOffset(1)]
            //private byte _b1;
            //[FieldOffset(2)]
            //private byte _b2;
            //[FieldOffset(3)]
            //private byte _b3;
            //[FieldOffset(0)]
            //private short _s0;
            //[FieldOffset(2)]
            //private short _s1;
            //[FieldOffset(0)]
            internal int _i;
            internal Vector32(byte b0, byte b1, byte b2, byte b3) =>
                _i = (b3 << 24) | (b2 << 16) | (b1 << 8) | b0;
            //internal void Init(short s0, short s1) =>
            //    _i = ((ushort)s1 << 16) | (ushort)s0;
            //internal void Init(short s)
            //    => Init(s, s);
            //internal void InitScalar(short s)
            //    => _i = s;
            internal Vector32(int i) => _i = i;
        }
    }
}
