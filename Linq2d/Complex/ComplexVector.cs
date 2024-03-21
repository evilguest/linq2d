using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Linq2d.ComplexVector
{
    using System;
    using System.Numerics;
    using System.Runtime.InteropServices;

    //public struct Vector256Complex
    //{
    //    private Vector256<double> _data;
    //    private Vector256Complex(Vector256<double> data) => _data = data;
    //    public static Vector256Complex Create(Complex e1, Complex e2) 
    //        => new Vector256Complex(Vector256.Create(e1.Real, e1.Imaginary, e2.Real, e2.Imaginary));
    //    public static Vector256Complex Create(double e1, double e2)
    //        => new Vector256Complex(Vector256.Create(e1, 0, e2, 0));
    //    public static Vector256Complex Zero { get => Create(0, 0); }
    //    public static Vector256Complex One { get => Create(1, 1); }

    //    public static Vector256Complex operator +(Vector256Complex left, Vector256Complex right)
    //    {
    //        var d = Avx.Add(left._data, right._data);
    //        return Unsafe.As<Vector256Complex>(d);
    //    }
    //}
    public static class ComplexAvx
    {
        public static Span<Complex> Add(ReadOnlySpan<Complex> left, ReadOnlySpan<Complex> right)
        {
            var result = new Complex[Math.Min(left.Length, right.Length)].AsSpan();
            var vectorRes = MemoryMarshal.Cast<Complex, Vector256<double>>(result);
            var vectorLeft = MemoryMarshal.Cast<Complex, Vector256<double>>(left);
            var vectorRight = MemoryMarshal.Cast<Complex, Vector256<double>>(right);
            for (int i = 0; i < vectorRes.Length; i++)
                vectorRes[i] = Avx.Add(vectorLeft[i], vectorRight[i]);

            for (int i = 2 * vectorRes.Length; i < result.Length; i++)
                result[i] = left[i] + right[i];
            return result;
        }

        public static Span<Complex> Subtract(ReadOnlySpan<Complex> left, ReadOnlySpan<Complex> right)
        {
            var result = new Complex[Math.Min(left.Length, right.Length)].AsSpan();
            var vectorRes = MemoryMarshal.Cast<Complex, Vector256<double>>(result);
            var vectorLeft = MemoryMarshal.Cast<Complex, Vector256<double>>(left);
            var vectorRight = MemoryMarshal.Cast<Complex, Vector256<double>>(right);
            for (int i = 0; i < vectorRes.Length; i++)
                vectorRes[i] = Avx.Subtract(vectorLeft[i], vectorRight[i]);

            for (int i = 2 * vectorRes.Length; i < result.Length; i++)
                result[i] = left[i] - right[i];
            return result;
        }

        public static Span<Complex> Multiply(ReadOnlySpan<Complex> left, ReadOnlySpan<Complex> right)
        {
            var result = new Complex[Math.Min(left.Length, right.Length)].AsSpan();
            var vectorRes = MemoryMarshal.Cast<Complex, Vector256<double>>(result);
            var vectorLeft = MemoryMarshal.Cast<Complex, Vector256<double>>(left);
            var vectorRight = MemoryMarshal.Cast<Complex, Vector256<double>>(right);
            for (int i = 0; i < vectorRes.Length; i++)
            {
                var l = vectorLeft[i];  
                var r = vectorRight[i]; 
                vectorRes[i] = Avx.HorizontalAdd(
                    Avx.Multiply(
                        Avx.Multiply(l, r), 
                        Vector256.Create(1.0, -1.0, 1.0, -1.0)), 
                    Avx.Multiply(
                        l, 
                        Avx.Permute(r, 0b0101)
                        )); 
            }
            for (int i = 2 * vectorRes.Length; i < result.Length; i++)
                result[i] = left[i] * right[i];
            return result;
        }

    }
}
