using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private List<Rectangle> addedRectangles;
        private Spiral spiral;

        public CircularCloudLayouter(Point center)
        {
            addedRectangles = new List<Rectangle>();
            spiral = new Spiral(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var location = spiral.CalculateNewLocation();
            var newRect = new Rectangle(location, rectangleSize);
            while (!IsCorrectPlaced(newRect))
                newRect = new Rectangle(spiral.CalculateNewLocation(), rectangleSize);
            addedRectangles.Add(newRect);
            return newRect;
        }

        private bool IsCorrectPlaced(Rectangle rect)
        {
            return addedRectangles.All(addedRec => !addedRec.IntersectsWith(rect));
        }
    }
}