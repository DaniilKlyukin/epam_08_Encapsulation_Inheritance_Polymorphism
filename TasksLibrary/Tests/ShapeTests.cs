namespace Tests
{
    using NUnit.Framework;
    using TasksLibrary;
    using System;

    [TestFixture]
    class ShapeTests
    {

        #region CircleTests

        [TestCase(0)]
        [TestCase(-10)]
        public void UnrealCircleTest(double radius)
        {
            Assert.Throws<ArgumentException>(() => new Circle(radius));
        }

        [TestCase(1, ExpectedResult = 2 * Math.PI)]
        [TestCase(3, ExpectedResult = 2 * Math.PI * 3)]
        [TestCase(10, ExpectedResult = 2 * Math.PI * 10)]
        public double CirclePerimeterTest(double radius)
        {
            return (new Circle(radius)).Perimeter;
        }

        [TestCase(1, ExpectedResult = Math.PI)]
        [TestCase(3, ExpectedResult = Math.PI * 3 * 3)]
        [TestCase(10, ExpectedResult = Math.PI * 10 * 10)]
        public double CircleAreaTest(double radius)
        {
            return (new Circle(radius)).Area;
        }

        #endregion

        #region TriangleTests

        [TestCase(0, 1, 1)]
        [TestCase(-3, 4, 5)]
        [TestCase(-3, -4, 5)]
        [TestCase(0, 0, 0)]
        [TestCase(1, 10, 1)]
        public void UnrealTriangleTest(double a, double b, double c)
        {
            Assert.Throws<ArgumentException>(() => new Triangle(a, b, c));
        }

        [TestCase(2, 3, 4, ExpectedResult = 9)]
        [TestCase(100, 50, 70, ExpectedResult = 220)]
        [TestCase(3, 3, 3, ExpectedResult = 9)]
        public double TrianglePerimeterTest(double a, double b, double c)
        {
            return (new Triangle(a, b, c)).Perimeter;
        }

        [TestCase(2, 3, 4, 2.905)]
        [TestCase(100, 50, 70, 1624.808)]
        [TestCase(3, 3, 3, 3.897)]
        public void TriangleAreaTest(double a, double b, double c, double expected)
        {
            Assert.AreEqual(
                expected,
                (new Triangle(a, b, c)).Area,
                1e-3);
        }

        #endregion

        #region RectangleTests

        [TestCase(0, 1)]
        [TestCase(-3, 4)]
        [TestCase(-3, -4)]
        [TestCase(0, 0)]
        public void UnrealRectangleTest(double side1, double side2)
        {
            Assert.Throws<ArgumentException>(() => new Rectangle(side1, side2));
        }

        [TestCase(2, 3, ExpectedResult = 10)]
        [TestCase(100, 50, ExpectedResult = 300)]
        [TestCase(3, 3, ExpectedResult = 12)]
        public double RectanglePerimeterTest(double side1, double side2)
        {
            return (new Rectangle(side1, side2)).Perimeter;
        }

        [TestCase(2, 3, ExpectedResult = 6)]
        [TestCase(100, 50, ExpectedResult = 5000)]
        [TestCase(3, 3, ExpectedResult = 9)]
        public double RectangleAreaTest(double side1, double side2)
        {
            return (new Rectangle(side1, side2)).Area;
        }

        #endregion

        #region SquareTests

        [TestCase(0)]
        [TestCase(-3)]
        public void UnrealSquareTest(double side)
        {
            Assert.Throws<ArgumentException>(() => new Square(side));
        }

        [TestCase(2, ExpectedResult = 8)]
        [TestCase(100, ExpectedResult = 400)]
        [TestCase(1, ExpectedResult = 4)]
        public double SquarePerimeterTest(double side)
        {
            return (new Square(side)).Perimeter;
        }

        [TestCase(2, ExpectedResult = 4)]
        [TestCase(100, ExpectedResult = 10000)]
        [TestCase(1, ExpectedResult = 1)]
        public double SquareAreaTest(double side)
        {
            return (new Square(side)).Area;
        }

        #endregion
    }
}
