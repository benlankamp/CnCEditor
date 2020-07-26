using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CnCEditor.GUI
{
    public static class FilePreviews
    {
        public static Bitmap CenteredBitmap(Bitmap inputImage)
        {
            int iconSize = 64;

            Bitmap centeredImage = new Bitmap(iconSize, iconSize);

            var ratioX = (double)iconSize / inputImage.Width;
            var ratioY = (double)iconSize / inputImage.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(inputImage.Width * ratio);
            var newHeight = (int)(inputImage.Height * ratio);

            using (var graphics = Graphics.FromImage(centeredImage))
            {
                // Calculate x and y which center the image
                int y = (iconSize / 2) - newHeight / 2;
                int x = (iconSize / 2) - newWidth / 2;

                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.DrawImage(inputImage, x, y, newWidth, newHeight);
            }

            return centeredImage;
        }
    }
}
