using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public static class PointExtensions
    {
        public static int GetDistanceToZero(this Point point)
            => (int) Math.Sqrt(point.X * point.X + point.Y * point.Y);
    }
}