namespace ImageHelpers.Tests
{
    public class ImageHelpers
    {
        [Theory]
        [InlineData("GrayGradient.bmp")]
        [InlineData("ColorGradient.bmp")]
        [InlineData("p00743.png")]
        public void ReadBmp(string fileName)
        {
            var s = IO.ReadImage(fileName);
        }

        [Theory]
        [InlineData("GrayGradient.bmp")]
        [InlineData("ColorGradient.bmp")]
        [InlineData("p00743.png")]
        public void GrayScaleBmp(string fileName)
        {
            var s = IO.ReadGrayScale8(fileName);
            IO.WriteImage(Path.GetFileNameWithoutExtension(fileName) + ".gray.png", s);
        }

    }
}