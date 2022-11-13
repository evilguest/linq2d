using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2d.MathHelpers
{
    public static class Fast
    {
        public static short Add(short left, short right) => (short)(left + right);
        public static ushort Add(ushort left, ushort right) => (ushort)(left + right);
        public static short Subtract(short left, short right) => (short)(left - right);
        public static ushort Subtract(ushort left, ushort right) => (ushort)(left - right);
        public static short Multiply(short left, short right) => (short)(left * right);
        public static ushort Multiply(ushort left, ushort right) => (ushort)(left * right);
    }
}
