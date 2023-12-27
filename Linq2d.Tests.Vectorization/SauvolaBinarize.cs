using System;
using Xunit;
using Linq2d.Tests.Vectorization;
using Square = System.Int64;

namespace Linq2d.Tests
{
    public class SauvolaBinarize: IClassFixture<VectorizationStateFixture>
    {

        [Theory]
        [InlineData(50, 50, 2)]
        [InlineData(100, 100, 5)]
        [InlineData(1000, 2151, 5)]
        [InlineData(170, 70, 5)]
        public void TestSauvolaSynthetic(int h, int w, byte seed)
        {
            var data = ArrayHelper.InitAllRand(h, w, seed);
            //Array2d.SaveDynamicCode = true;
            Assert.Equal(BaseBinarize(data), LinqBinarize(data));
            //Array2d.SaveDynamicCode = false;
        }

        [Theory]
        [InlineData(50, 50, 2)]
        public void TestSauvolaWithoutVariablePooling(int h, int w, byte seed)
        {
            var data = ArrayHelper.InitAllRand(h, w, seed);
            Array2d.PoolCSEVariables = false;
            Assert.Equal(BaseBinarize(data), LinqBinarize(data));
            Array2d.PoolCSEVariables = true;
        }


        [Theory]
        [InlineData(50, 50, 42)]
        [InlineData(100, 100, 42)]
        [InlineData(1000, 2151, 42)]
        [InlineData(170, 70, 42)]
        public unsafe void TestCppSauvolaSynthetic(int h, int w, byte seed)
        {
            var data = ArrayHelper.InitAllRand(h, w, seed);

            var cs = DoubleIntegrateSlow(h, w, data);

            Assert.Equal(BaseBinarize(data), UnmanagedSauvola.Transform(data, W / 2, K));
        }


        public static double K { get; private set; } = 0.1;
        public static byte W { get; private set; } = 10;

        [Theory]
        [InlineData(17, 17, 5)]
        public void TestSteps(int h, int w, byte seed)
        {
            var grayImage = ArrayHelper.InitAllRand(h, w, seed);

            var integral = from g in grayImage
                           from ri in Result.InitWith(0)
                           from rq in Result.InitWith((Square)0)
                           select ValueTuple.Create(
                               ri[-1, 0] + ri[0, -1] - ri[-1, -1] + g,
                               rq[-1, 0] + rq[0, -1] - rq[-1, -1] + g * g);

            var (integral_image_linq, integral_sqimg_linq) = integral.ToArrays();

            var (integral_image, integral_sqimg) = DoubleIntegrateSlow(h, w, grayImage);

            Assert.Equal(integral_image, integral_image_linq);
            Assert.Equal(integral_sqimg, integral_sqimg_linq);

            var whalf = W / 2;
            var area = from i in integral_image_linq.With(OutOfBoundsStrategy.Integral(0))
                       let tl = i.Offset(-whalf - 1, -whalf - 1)
                       let br = i.Offset(whalf, whalf)
                       select (br.X - tl.X) * (br.Y - tl.Y);
            var areaA = area.ToArray();
            Assert.Equal(CalculateArea(grayImage, whalf), area.ToArray());
            var diff = from i in integral_image_linq.With(OutOfBoundsStrategy.Integral(0))
                       let tl = i.Offset(-whalf - 1, -whalf - 1)
                       let tr = i.Offset(-whalf - 1, whalf)
                       let bl = i.Offset(whalf, -whalf - 1)
                       let br = i.Offset(whalf, whalf)
                       select br.Value + tl.Value - tr.Value - bl.Value;
            var diffA = diff.ToArray();
            Assert.Equal(CalculateDiff(integral_image_linq, whalf), diff.ToArray());

            var sqdiff = from i in integral_sqimg_linq.With(OutOfBoundsStrategy.Integral(0L))
                         let tl = i.Offset(-whalf - 1, -whalf - 1)
                         let tr = i.Offset(-whalf - 1, whalf)
                         let bl = i.Offset(whalf, -whalf - 1)
                         let br = i.Offset(whalf, whalf)
                         select br.Value + tl.Value - tr.Value - bl.Value;
            Assert.Equal(CalculateDiff(integral_sqimg_linq, whalf), sqdiff.ToArray());

            var mean = from d in diffA
                       from a in areaA
                       select (double)d / a;

            Assert.Equal(CalculateMean(diffA, areaA), mean.ToArray());

            var std = from sqd in sqdiff.ToArray()
                      from d in diffA
                      from a in areaA
                      from m in mean.ToArray()
                      select Math.Sqrt((sqd - d * m) / (a - 1));

            Assert.Equal(CalculateStd(sqdiff.ToArray(), diffA, areaA, mean.ToArray()), std.ToArray());

            var thresh = from m in mean.ToArray()
                         from s in std.ToArray()
                         select m * (1 + K * ((s / 128) - 1));
            Assert.Equal(CalculateThreshold(mean.ToArray(), std.ToArray()), thresh.ToArray());

        }
        [Fact]
        public void TestStepsWithoutAvx2()
        {
            CodeGen.Intrinsics.Avx2.Suppress = true;
            TestSteps(17, 17, 5);
            CodeGen.Intrinsics.Avx2.Suppress = false;
        }
        [Fact]
        public void TestStepsWithoutAvx()
        {
            CodeGen.Intrinsics.Avx.Suppress = true;
            TestSteps(17, 17, 5);
            CodeGen.Intrinsics.Avx.Suppress = false;
        }

        [Fact]
        public void TestStepsWithoutSse2()
        {
            CodeGen.Intrinsics.Sse2.Suppress = true;
            TestSteps(17, 17, 5);
            CodeGen.Intrinsics.Sse2.Suppress = false;
        }
        [Fact]
        public void TestStepsWithoutSse()
        {
            CodeGen.Intrinsics.Sse.Suppress = true;
            TestSteps(17, 17, 5);
            CodeGen.Intrinsics.Sse.Suppress = false;
        }

        private double[,] CalculateThreshold(double[,] mean, double[,] std)
        {
            var (h, w) = ArrayHelper.EnsureSize(0, 0, mean, std);
            var result = new double[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    result[i, j] = mean[i, j] * (1 + K * ((std[i, j] / 128) - 1));

            return result;
        }

        private double[,] CalculateStd(Square[,] sqdiff, int[,] diff, int[,] area, double[,] mean)
        {
            var (h, w) = ArrayHelper.EnsureSize(0, 0, sqdiff, diff, area, mean);
            var result = new double[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    result[i, j] = Math.Sqrt((sqdiff[i, j] - diff[i, j] * mean[i, j]) / (area[i, j] - 1));

            return result;
        }

        private double[,] CalculateMean(int[,] diff, int[,] area)
        {
            var (h, w) = ArrayHelper.EnsureSize(0, 0, diff, area);
            var result = new double[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    result[i, j] = (double)diff[i, j] / area[i, j];
            return result;
        }

        private static int[,] CalculateArea(byte[,] grayImage, int whalf)
        {
            var result = new int[grayImage.Height(), grayImage.Width()];
            for (int i = 0; i < result.Height(); i++)
                for (int j = 0; j < result.Width(); j++)
                {
                    var xmin = Math.Max(0, i - whalf);
                    var ymin = Math.Max(0, j - whalf);
                    var xmax = Math.Min(result.Height() - 1, i + whalf);
                    var ymax = Math.Min(result.Width() - 1, j + whalf);

                    result[i, j] = (xmax - xmin + 1) * (ymax - ymin + 1);

                }
            return result;
        }

        private static Square[,] CalculateDiff(Square[,] integralImage, int whalf)
        {
            var result = new Square[integralImage.Height(), integralImage.Width()];
            for (int i = 0; i < result.Height(); i++)
                for (int j = 0; j < result.Width(); j++)
                {
                    var xmin = Math.Max(0, i - whalf);
                    var ymin = Math.Max(0, j - whalf);
                    var xmax = Math.Min(result.Height() - 1, i + whalf);
                    var ymax = Math.Min(result.Width() - 1, j + whalf);

                    if (xmin == 0 && ymin == 0)
                        result[i, j] = integralImage[xmax, ymax];
                    else if (xmin == 0)
                        result[i, j] = integralImage[xmax, ymax] - integralImage[xmax, ymin - 1];
                    else if (ymin == 0)
                        result[i, j] = integralImage[xmax, ymax] - integralImage[xmin - 1, ymax];
                    else
                        result[i, j] = integralImage[xmax, ymax] - integralImage[xmin - 1, ymax] - integralImage[xmax, ymin - 1] + integralImage[xmin - 1, ymin - 1];
                }
            return result;
        }
        private static int[,] CalculateDiff(int[,] integralImage, int whalf)
        {
            var result = new int[integralImage.Height(), integralImage.Width()];
            for (int i = 0; i < result.Height(); i++)
                for (int j = 0; j < result.Width(); j++)
                {
                    var xmin = Math.Max(0, i - whalf);
                    var ymin = Math.Max(0, j - whalf);
                    var xmax = Math.Min(result.Height() - 1, i + whalf);
                    var ymax = Math.Min(result.Width() - 1, j + whalf);

                    if (xmin == 0 && ymin == 0)
                        result[i, j] = integralImage[xmax, ymax];
                    else if (xmin == 0)
                        result[i, j] = integralImage[xmax, ymax] - integralImage[xmax, ymin - 1];
                    else if (ymin == 0)
                        result[i, j] = integralImage[xmax, ymax] - integralImage[xmin - 1, ymax];
                    else
                        result[i, j] = integralImage[xmax, ymax] - integralImage[xmin - 1, ymax] - integralImage[xmax, ymin - 1] + integralImage[xmin - 1, ymin - 1];
                }
            return result;
        }

        private static (int[,], Square[,]) DoubleIntegrateSlow(int h, int w, byte[,] grayImage)
        {
            var integral_image = new int[h, w];
            var integral_sqimg = new Square[h, w];
            integral_image[0, 0] = grayImage[0, 0];
            integral_sqimg[0, 0] = grayImage[0, 0] * grayImage[0, 0];

            for (int j = 1; j < w; j++)
            {
                integral_image[0, j] = grayImage[0, j] + integral_image[0, j - 1];
                integral_sqimg[0, j] = grayImage[0, j] * grayImage[0, j] + integral_sqimg[0, j - 1];
            }

            for (int i = 1; i < h; i++)
            {
                integral_image[i, 0] = grayImage[i, 0] + integral_image[i - 1, 0];
                integral_sqimg[i, 0] = grayImage[i, 0] * grayImage[i, 0] + integral_sqimg[i - 1, 0];

                for (int j = 1; j < w; j++)
                {
                    integral_image[i, j] = grayImage[i, j]
                        + integral_image[i - 1, j] + integral_image[i, j - 1] - integral_image[i - 1, j - 1];
                    integral_sqimg[i, j] = grayImage[i, j] * grayImage[i, j]
                        + integral_sqimg[i - 1, j] + integral_sqimg[i, j - 1] - integral_sqimg[i - 1, j - 1];
                }
            }
            return (integral_image, integral_sqimg);
        }

        private byte[,] LinqBinarize(byte[,] grayImage)
        {

            var integral = from g in grayImage
                           from ri in Result.InitWith(0)
                           from rq in Result.InitWith(0L)
                           select ValueTuple.Create(
                               ri[-1, 0] + ri[0, -1] - ri[-1, -1] + g,
                               rq[-1, 0] + rq[0, -1] - rq[-1, -1] + g * g);

            var (integral_image, integral_sqimg) = integral.ToArrays();


            var whalf = W / 2;

            var t = from g in grayImage
                    from i in integral_image.With(OutOfBoundsStrategy.Integral(0))
                    from q in integral_sqimg.With(OutOfBoundsStrategy.Integral(0L)) 
                    let tl = i.Offset(-whalf-1, -whalf-1)  let tr = i.Offset(-whalf-1, whalf)
                    let bl = i.Offset(whalf, -whalf-1)   let br = i.Offset(whalf, whalf)
                    let tlq = q.Offset(-whalf-1, -whalf-1) let trq = q.Offset(-whalf-1, whalf)
                    let blq = q.Offset(whalf, -whalf-1)  let brq = q.Offset(whalf, whalf)
                    let area = (br.X - tl.X) * (br.Y - tl.Y)
                    let diff = br + tl - tr - bl
                    let sqdiff = brq.Value + tlq.Value - trq.Value - blq.Value
                    let mean = (double)diff / area
                    let std = Math.Sqrt((sqdiff - diff * mean) / (area - 1))

                    let threshold = mean * (1 + K * ((std / 128) - 1))

                    select g > threshold ? (byte)255 : (byte)0;


            return t.ToArray();
        }

        public static byte[,] BaseBinarize(byte[,] grayImage)
        {
            int image_height = grayImage.GetLength(0);
            int image_width = grayImage.GetLength(1);
            var integral_image = ArrayHelper.Create<double>(image_height + 1, image_width + 1, -1, -1);
            var integral_sqimg = ArrayHelper.Create<double>(image_height + 1, image_width + 1, -1, -1);

            var result = new byte[image_height, image_width];

            for (int i = 0; i < image_height; i++)
                for (int j = 0; j < image_width; j++)
                {
                    integral_image[i, j] = grayImage[i, j] 
                        + integral_image[i - 1, j] + integral_image[i, j - 1] - integral_image[i - 1, j - 1]; ;
                    integral_sqimg[i, j] = grayImage[i, j] * grayImage[i, j] 
                        + integral_sqimg[i - 1, j] + integral_sqimg[i, j - 1] - integral_sqimg[i - 1, j - 1];
                }

            var whalf = W / 2;

            for (int i = 0; i < image_height; i++)
                for (int j = 0; j < image_width; j++)
                {
                    var xmin = Math.Max(0, i - whalf);
                    var ymin = Math.Max(0, j - whalf);
                    var xmax = Math.Min(image_height - 1, i + whalf);
                    var ymax = Math.Min(image_width - 1, j + whalf);

                    var area = (xmax - xmin + 1) * (ymax - ymin + 1);

                    var diff   = integral_image[xmax, ymax] - integral_image[xmin - 1, ymax] - integral_image[xmax, ymin - 1] + integral_image[xmin - 1, ymin - 1];
                    var sqdiff = integral_sqimg[xmax, ymax] - integral_sqimg[xmin - 1, ymax] - integral_sqimg[xmax, ymin - 1] + integral_sqimg[xmin - 1, ymin - 1];
                    var mean = (double)diff / area;
                    var std = Math.Sqrt((sqdiff - diff * mean) / (area - 1));

                    var threshold = mean * (1 + K * ((std / 128) - 1));

                    result[i, j] = grayImage[i, j] > threshold ? (byte)255 : (byte)0;
                }

            return result;
        }
    }
}
