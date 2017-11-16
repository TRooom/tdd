using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    internal class Spiral
    {
        private readonly Point center;
        private readonly double a;
        private readonly double phi;
        private double length = 0;

        public Spiral(Point center, double a = 5, double phi = 50)
        {
            this.center = center;
            this.a = a;
            this.phi = phi;
        }

        public Point CalculateNewLocation(double step = 0.01)
        {
            var x = (int) (length * a * Math.Cos(length * phi)) + center.X;
            var y = (int) (length * a * Math.Sin(length * phi)) + center.Y;
            length += step;
            return new Point(x, y);
        }

        public IEnumerable<Point> GetAllPoints(double step = 0.01)
        {
            var length = 0d;
            while (true)
            {
                var x = (int) (length * a * Math.Cos(length * phi)) + center.X;
                var y = (int) (length * a * Math.Sin(length * phi)) + center.Y;
                length += step;
                yield return new Point(x, y);
            }
        }
    }
}