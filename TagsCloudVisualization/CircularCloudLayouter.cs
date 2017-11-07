using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private Point center;
        private const int a = 5;
        private double t;
        private List<Rectangle> addedRectangles;

        public CircularCloudLayouter(Point center)
        {
            this.center = center;
            addedRectangles = new List<Rectangle>();
        }

        private Point CalculateNewLocation()
        {
            var x = (int) (t * a * Math.Cos(t * 50)) + center.X;
            var y = (int) (t * a * Math.Sin(t * 50)) + center.Y;
            t += 0.1;
            return new Point(x, y);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var newRect = new Rectangle(CalculateNewLocation(), rectangleSize);
            while (!IsCorrectPlaced(newRect))
                newRect = new Rectangle(CalculateNewLocation(), rectangleSize);
            addedRectangles.Add(newRect);
            return newRect;
        }

        private bool IsCorrectPlaced(Rectangle rect)
        {
            return addedRectangles.All(addedRec => !addedRec.IntersectsWith(rect));
        }
    }
}