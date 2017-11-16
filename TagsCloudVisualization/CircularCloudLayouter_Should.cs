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
        private CircularCloudLayouter defaultLayouter;
        private string testDirectory = TestContext.CurrentContext.TestDirectory;
        private string logsDirectory;
        private Size defaultSize;
        private List<Rectangle> rectangles;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            logsDirectory = Path.Combine(testDirectory, "Logs");
            Directory.CreateDirectory(logsDirectory);
        }

        [SetUp]
        public void SetUp()
        {
            rectangles = new List<Rectangle>();
            defaultLayouter = new CircularCloudLayouter(new Point(0, 0));
            defaultSize = new Size(10, 10);
        }

        [Test]
        public void AddRectangleSavingSize()
        {
            var rect = AddRectangleInCloud(defaultSize);
            rect.Size.Should().Be(defaultSize);
        }

        [Test, Timeout(1000)]
        public void AddTwoRectangles_InDifferentPoints()
        {
            var rect1 = AddRectangleInCloud(defaultSize);
            var rect2 = AddRectangleInCloud(defaultSize);
            rect1.Location.Should().NotBe(rect2.Location);
        }

        [Test, Timeout(1000)]
        public void AddTwoRectangles_WithoutCrossing()
        {
            var rect1 = AddRectangleInCloud(defaultSize);
            var rect2 = AddRectangleInCloud(defaultSize);
            rect1.IntersectsWith(rect2).Should().BeFalse();
        }

        [Test]
        public void AddRectangtesTight()
        {
            var added = new List<Rectangle>();
            var lasAdded = new Point();
            var size = new Size(15, 15);
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
        }

        public Rectangle AddRectangleInCloud(Size size)
        {
            var rect = defaultLayouter.PutNextRectangle(size);
            rectangles.Add(rect);
            return rect;
        }

        [Test, Explicit]
        public void GenerateAndSaveCloud()
        {
            var rnd = new Random();
            for (int i = 0; i < 1000; i++)
                AddRectangleInCloud(new Size(rnd.Next(10, 50), rnd.Next(10, 50)));
            Assert.Fail();
        }

        [TearDown]
        public void TearDown()
        {
            var name = TestContext.CurrentContext.Test.Name;
            var time = DateTime.Now.ToString("dd-hh-mm-ss", CultureInfo.InvariantCulture);
            var path = Path.Combine(logsDirectory, $"{name};{time}.png");
            var testResult = TestContext.CurrentContext.Result.Outcome;
            if (!testResult.Equals(ResultState.Failure) && !testResult.Equals(ResultState.Error)) return;
            CloudVisualizator.SaveCloud(rectangles, path);
            Console.WriteLine($"Tag cloud visualization saved to file {Path.Combine(testDirectory, path)}");
        }
    }
}