using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace OpenCVSharpSample05Wpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            loadImage();
        }

		public static BitmapSource Convert(System.Drawing.Bitmap bitmap)
		{
			var bitmapData = bitmap.LockBits(
				new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
				System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

			var bitmapSource = BitmapSource.Create(
				bitmapData.Width, bitmapData.Height,
				bitmap.HorizontalResolution, bitmap.VerticalResolution,
				PixelFormats.Bgr24, null,
				bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

			bitmap.UnlockBits(bitmapData);
			return bitmapSource;
		}

		private void loadImage()
        {
            using (var iplImage = new Mat(@"..\..\Images\Penguin.png", ImreadModes.AnyDepth | ImreadModes.AnyColor))
            {
                Cv2.Dilate(iplImage, iplImage, new Mat());

                //Image1.Source = iplImage.ToWriteableBitmap(PixelFormats.Bgr24);
				Image1.Source = Convert( OpenCvSharp.Extensions.BitmapConverter.ToBitmap(iplImage));

			}
        }
    }
}