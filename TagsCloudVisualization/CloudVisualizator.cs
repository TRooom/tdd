using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization
{
    public static class CloudVisualizator
    {
        public static void SaveCloud(List<Rectangle> tags, string path, Color background = default(Color), Pen pen = null)
        {
            var imageSize = GetSize(tags);
            var image = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(image);
            graphics.Clear(background);
            pen = pen ?? new Pen(Color.BlueViolet, 1);
            foreach (var tag in tags)
            {
                var newX = tag.X + imageSize.Width / 2;
                var newY = tag.Y + imageSize.Height / 2;
                graphics.DrawRectangle(pen, new Rectangle(new Point(newX, newY), tag.Size));
            }
            image.Save(path, ImageFormat.Png);
           Console.Write("");
        }

        private static Size GetSize(List<Rectangle> rectangles)
        {
            var yMax = int.MinValue;
            var xMax = int.MinValue;
            foreach (var rect in rectangles)
            {
                if (rect.X + rect.Width > xMax)
                    xMax = rect.X + rect.Width;
                if (rect.Y + rect.Height > yMax)
                    yMax = rect.Y + rect.Height;
            }
            return new Size(xMax * 3, yMax * 3);
        }
    }
}