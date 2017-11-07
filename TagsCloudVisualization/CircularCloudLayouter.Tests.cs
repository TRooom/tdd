using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class TagsCloudVisualization_Should
    {
        private CircularCloudLayouter defaultLayouter;
        private Size defaultSize;

        [SetUp]
        public void SetUp()
        {
            defaultLayouter = new CircularCloudLayouter(new Point(0, 0));
            defaultSize = new Size(10, 10);
        }

        [Test]
        public void AddRectangleSavingSize()
        {
            var rect = defaultLayouter.PutNextRectangle(defaultSize);
            rect.Size.Should().Be(defaultSize);
        }

        [Test, Timeout(100)]
        public void AddTwoRectangles_InDifferentPoints()
        {
            var rect1 = defaultLayouter.PutNextRectangle(defaultSize);
            var rect2 = defaultLayouter.PutNextRectangle(defaultSize);
            rect1.Location.Should().NotBe(rect2.Location);
        }

        [Test, Timeout(100)]
        public void AddTwoRectangles_WithoutCrossing()
        {
            var rect1 = defaultLayouter.PutNextRectangle(defaultSize);
            var rect2 = defaultLayouter.PutNextRectangle(defaultSize);
            rect1.IntersectsWith(rect2).Should().BeFalse();
        }

        [Test]
        public void AddRectangtesTight()
        {
            var added = new List<Rectangle>();
            Point lasAdded = new Point();
            for (int i = 0; i < 100; i++)
            {
                var rect = defaultLayouter.PutNextRectangle(new Size(2, 2));
                added.Add(rect);
                lasAdded = rect.Location;
            }
            var distance = Math.Sqrt(lasAdded.Y * lasAdded.Y + lasAdded.X * lasAdded.X);
            distance.Should().BeLessThan((int) ((100 / 4) * Math.Sqrt(8)));
        }
    }
}