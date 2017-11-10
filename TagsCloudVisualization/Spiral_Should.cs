using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    class Spiral_Should
    {
        private Spiral defaultSpiral;

        [SetUp]
        public void SetUp()
        {
            defaultSpiral = new Spiral(new Point(0, 0));
        }

        [Test]
        public void DefaultSpiral_ReturnZero_WhenFirstCalled()
        {
            var point = defaultSpiral.CalculateNewLocation();
            point.Should().Be(new Point(0, 0));
        }

        [Test]
        public void CustomSpiral_ReturnCenter_WhenFirstCalled()
        {
            var center = new Point(2, 5);
            var customSpiral = new Spiral(center);
            var point = customSpiral.CalculateNewLocation();
            point.Should().Be(center);
        }

        [Test, Timeout(100)]
        public void ReturnDistantPoint_ForNextCall()
        {
            var point = default(Point);
            var nextPoint = defaultSpiral.CalculateNewLocation();
            for (int i = 0; i < 20; i++)
                nextPoint = defaultSpiral.CalculateNewLocation();
            point.GetDistanceToZero().Should().BeLessThan(nextPoint.GetDistanceToZero());
        }
    }
}