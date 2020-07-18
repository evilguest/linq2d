using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageHelpers
{
    public class IO
    {
        public unsafe static byte[,] ReadImage(string fileName)
        {
            using (var b = new Bitmap(fileName))
            {
                BitmapData d = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                try
                {
                    Console.WriteLine($"Found the bitmap of {d.Width}*{d.Height}");
                    var data = new byte[d.Height, d.Stride];
                    fixed (byte* pData = &data[0, 0])
                    {
                        Buffer.MemoryCopy((byte*)d.Scan0, pData, data.LongLength, d.Height * d.Stride);
                    }
                    return data;
                }
                finally
                {
                    b.UnlockBits(d);
                }
            }
        }
        public unsafe static void WriteImage(string fileName, byte[,] data)
        {
            using (var b = new Bitmap(data.GetLength(1), data.GetLength(0), PixelFormat.Format8bppIndexed))
            {
                var p = b.Palette;
                for (int i = 0; i < 256; i++)
                    p.Entries[i] = Color.FromArgb(i, i, i);
                b.Palette = p;
                BitmapData d = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                try
                {
                    fixed (byte* pData = &data[0, 0])
                        Buffer.MemoryCopy(pData, (byte*)d.Scan0, d.Stride * d.Height, data.LongLength);
                }
                finally
                {
                    b.UnlockBits(d);
                }
                b.Save(fileName);
            }
        }
    }
}
