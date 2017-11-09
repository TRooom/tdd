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
        public Spiral DefaultSpiral;

        [SetUp]
        public void SetUp()
        {
            DefaultSpiral = new Spiral(new Point(0, 0));
        }

        [Test]
        public void DefaultSpiral_ReturnZero_WhenFirstCalled()
        {
            var point = DefaultSpiral.CalculateNewLocation();
            point.Should().Be(new Point(0, 0));
        }

        [Test]
        public void CastomSpiral_ReturnCenter_WhenFirstCalled()
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
            while (point.Equals(default(Point)))
                point = DefaultSpiral.CalculateNewLocation();
            var nextPoint = DefaultSpiral.CalculateNewLocation();
            for (int i = 0; i < 20; i++)
                nextPoint = DefaultSpiral.CalculateNewLocation();
            point.GetDistance().Should().BeLessThan(nextPoint.GetDistance());
        }
    }
}