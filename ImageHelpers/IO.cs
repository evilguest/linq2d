using System;
using System.IO;
using System.IO.Compression;
using SkiaSharp;

namespace ImageHelpers
{
    public static class IO
    {

        public static byte[,] AsGrayScale8(this SKBitmap bitmap)
        {
            var data = new byte[bitmap.Height, bitmap.Width];
            for (int i = 0; i < bitmap.Height; i++)
                for (int j = 0; j < bitmap.Width; j++)
                {
                    var p = bitmap.GetPixel(j, i);
                    data[i, j] = (byte)Math.Round(p.Red * 0.21f + p.Green * 0.72f + p.Blue * 0.07f);
                }
            return data;
        }

        public static byte[,] ReadGrayScale8(string fileName)
            => ReadImage(fileName).AsGrayScale8();

        public static SKBitmap ReadImage(string fileName)
        {
            using(var s = GetReadStream(fileName))
                return SKBitmap.Decode(s);
        }

        private static Stream GetReadStream(string fileName)
        {
            Stream stream = File.OpenRead(fileName);
            return Path.GetExtension(fileName) != ".gz" ? stream : new GZipStream(stream, CompressionMode.Decompress, false);

        }

        public unsafe static void WriteImage(string fileName, byte[,] grayscale8)
        {
            using(var s = File.OpenWrite(fileName))
            using (var b = new SKBitmap(grayscale8.GetLength(1), grayscale8.GetLength(0), SKColorType.Gray8, SKAlphaType.Opaque))
            {
                var p = b.GetPixels();
                var target = (byte*)p.ToPointer();
                fixed (byte* source = &grayscale8[0, 0])
                    Buffer.MemoryCopy(source, target, grayscale8.Length, b.GetPixelSpan().Length);
                b.Encode(s, SKEncodedImageFormat.Png, 255);
                s.Flush();
            }
        }
    }
}

