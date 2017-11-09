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
        private double step = 0;

        public Spiral(Point center, double a = 5, double phi = 50)
        {
            this.center = center;
            this.a = a;
            this.phi = phi;
        }

        public Point CalculateNewLocation()
        {
            var x = (int) (step * a * Math.Cos(step * phi)) + center.X;
            var y = (int) (step * a * Math.Sin(step * phi)) + center.Y;
            step += 0.1;
            return new Point(x, y);
        }
    }
}