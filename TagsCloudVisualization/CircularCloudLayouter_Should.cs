using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        public CircularCloudLayouter DefaultLayouter;
        public string TestDirectory = TestContext.CurrentContext.TestDirectory;
        public string LogsDirectory;
        public Size DefaultSize;
        public List<Rectangle> Rectangles;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            LogsDirectory = Path.Combine(TestDirectory, "Logs");
            Directory.CreateDirectory(LogsDirectory);
        }

        [SetUp]
        public void SetUp()
        {
            Rectangles = new List<Rectangle>();
            DefaultLayouter = new CircularCloudLayouter(new Point(0, 0));
            DefaultSize = new Size(10, 10);
        }

        [TearDown]
        public void TearDown()
        {
            var name = TestContext.CurrentContext.Test.Name;
            var time = DateTime.Now.ToString("dd-hh-mm-ss", CultureInfo.InvariantCulture);
            var path = Path.Combine(LogsDirectory, $"{name};{time}.png");
            var testResult = TestContext.CurrentContext.Result.Outcome;
            if (!testResult.Equals(ResultState.Failure) && !testResult.Equals(ResultState.Error)) return;
            CloudVisualizator.SaveCloud(Rectangles, path);
            Console.WriteLine($"Tag cloud visualization saved to file {Path.Combine(TestDirectory, path)}");
        }

        [Test]
        public void AddRectangleSavingSize()
        {
            var rect = AddRectangleInCloud(DefaultSize);
            rect.Size.Should().Be(DefaultSize);
        }

        [Test, Timeout(1000)]
        public void AddTwoRectangles_InDifferentPoints()
        {
            var rect1 = AddRectangleInCloud(DefaultSize);
            var rect2 = AddRectangleInCloud(DefaultSize);
            rect1.Location.Should().NotBe(rect2.Location);
        }

        [Test, Timeout(1000)]
        public void AddTwoRectangles_WithoutCrossing()
        {
            var rect1 = AddRectangleInCloud(DefaultSize);
            var rect2 = AddRectangleInCloud(DefaultSize);
            rect1.IntersectsWith(rect2).Should().BeFalse();
        }

        [Test]
        public void AddRectangtesTight()
        {
            var added = new List<Rectangle>();
            var lasAdded = new Point();
            var size = new Size(15,15);
            var diag = Math.Sqrt(size.Height * size.Height + size.Width * size.Width);
            var count = 100;
            for (int i = 0; i < count; i++)
            {
                var rect = AddRectangleInCloud(size);
                added.Add(rect);
                lasAdded = rect.Location;
            }
            var distance = Math.Sqrt(lasAdded.Y * lasAdded.Y + lasAdded.X * lasAdded.X);
            distance.Should().BeLessThan((int) ((diag * count / 4) * Math.Sqrt(8)));
            //Assert.Fail(); Fail test to create image
        }

        public Rectangle AddRectangleInCloud(Size size)
        {
            var rect = DefaultLayouter.PutNextRectangle(size);
            Rectangles.Add(rect);
            return rect;
        }
    }
}