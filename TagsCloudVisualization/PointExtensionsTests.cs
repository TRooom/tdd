using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class PointExtensionsTests
    {
        [Test]
        public void GetDistance_ForZero_ShouldReturnZero()
        {
            new Point(0, 0).GetDistanceToZero().Should().Be(0);
        }

        [Test]
        public void GetDistance_ShoudReturnCorrectDistance()
        {
            new Point(6, 8).GetDistanceToZero().Should().Be(10);
        }

        [Test]
        public void Get_Distance_ShoudReturnCorrectDistance_ForNegativePoint()
        {
            new Point(-4, -3).GetDistanceToZero().Should().Be(5);
        }

        [Test]
        public void Get_Distance_ShoudReturnCorrectDistance_ForFirstQuarter()
        {
            new Point(8, 15).GetDistanceToZero().Should().Be(17);
        }

        [Test]
        public void Get_Distance_ShoudReturnCorrectDistance_ForSecondQuarter()
        {
            new Point(-8, 15).GetDistanceToZero().Should().Be(17);
        }

        [Test]
        public void Get_Distance_ShoudReturnCorrectDistance_ForThirdQuarter()
        {
            new Point(8, -15).GetDistanceToZero().Should().Be(17);
        }

        [Test]
        public void Get_Distance_ShoudReturnCorrectDistance_ForFourthQuarter()
        {
            new Point(-8, -15).GetDistanceToZero().Should().Be(17);
        }
    }
}