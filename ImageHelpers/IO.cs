using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;


namespace ImageHelpers
{
    public class IO
    {
        public unsafe static (byte[,], byte[]) ReadImage(string fileName)
        {
            using(var s = GetReadStream(fileName))
            using (var b = new Bitmap(s))
            {
                var brightness = (from color in b.Palette.Entries select (byte)(color.GetBrightness() * byte.MaxValue)).ToArray();

                BitmapData d = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                try
                {
                    Console.WriteLine($"Found the bitmap of {d.Width}*{d.Height}");
                    var data = new byte[d.Height, d.Stride];
                    fixed (byte* pData = &data[0, 0])
                    {
                        Buffer.MemoryCopy((byte*)d.Scan0, pData, data.LongLength, d.Height * d.Stride);
                    }
                    return (data, brightness);
                }
                finally
                {
                    b.UnlockBits(d);
                }
            }
        }

        private static Stream GetReadStream(string fileName)
        {
            Stream stream = File.OpenRead(fileName);
            return Path.GetExtension(fileName) != ".gz" ? stream : new GZipStream(stream, CompressionMode.Decompress, false);

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
