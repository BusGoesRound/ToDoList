using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ToDoList.Classes
{
    class RecolorImage
    {
        public static Image Recolor(Image image)
        {
            BitmapSource? bitmapSource = image.Source as BitmapSource;
            FormatConvertedBitmap bitmapImage = new(bitmapSource, PixelFormats.Bgra32, null, 0);

            int width = bitmapImage.PixelWidth;
            int height = bitmapImage.PixelHeight;

            WriteableBitmap bitmap = new(width, height, 96, 96, PixelFormats.Bgra32, null);

            uint[] pixels = new uint[width * height];
            int red, green, blue, alpha;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int i = width * y + x;

                    red = 0;
                    green = 0;
                    blue = 0;
                    alpha = 255;

                    pixels[i] = (uint)((blue << 24) + (green << 16) + (red << 8) + alpha);
                }
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * 4, 0);

            image.Source = bitmap;
            return image;
        }
    }
}
