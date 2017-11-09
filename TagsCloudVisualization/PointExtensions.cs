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
        public static int GetDistance(this Point point)
            => point.X * point.X + point.Y * point.Y;
    }
}