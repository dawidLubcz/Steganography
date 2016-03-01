using System;
using System.Drawing;

namespace Dawid
{
    class CBitmapExt
    {
        static Random _random = new Random();
        static void Shuffle<T>(T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + (int)(_random.NextDouble() * (n - i));
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }

        public static Bitmap shuffleBitmap(Bitmap a_oBitmap)
        {
            Bitmap _oBitmapRes = new Bitmap(a_oBitmap.Width, a_oBitmap.Height);
            Color[] pixelArray = new Color[a_oBitmap.Height * a_oBitmap.Width];

            for (int i = 0; i < a_oBitmap.Height; ++i)
            {
                for (int j = 0; j < a_oBitmap.Width; ++j)
                {
                    pixelArray[i + j] = a_oBitmap.GetPixel(j, i);
                }
            }

            Shuffle(pixelArray);

            for (int i = 0; i < a_oBitmap.Height; ++i)
            {
                for (int j = 0; j < a_oBitmap.Width; ++j)
                {
                    _oBitmapRes.SetPixel(j, i, pixelArray[i + j]);
                }
            }

            return _oBitmapRes;
        }
    }
}
