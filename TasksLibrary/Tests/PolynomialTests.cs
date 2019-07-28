using TasksLibrary;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{

    [TestFixture]
    public class PolynomialTests
    {
        [Test]
        public void PolySimpleInitializationTest()
        {
            var p1 = new Polynomial(new Dictionary<int, double>() { { 46, 3 }, { 0, 1 } }); // 3*x^46 + 1

            var p2 = new Polynomial(new Dictionary<int, double>() { { 28, 7 }, { 1, 2 } }); // 7*x^28 + 2*x

            CollectionAssert.AreEqual(new Polynomial(new Dictionary<int, double>() { { 46, 3 }, { 28, 7 }, { 1, 2 }, { 0, 1 } }), p1 + p2);
        }

        [Test]
        public void PolyDivisionTest()
        {
            var p1 = new Polynomial(new Dictionary<int, double>() { { 3, 1 }, { 2, -12 }, { 0, -42 } }); // x^3 - 12*x^2 - 42

            var p2 = new Polynomial(new Dictionary<int, double>() { { 1, 1 }, { 0, -3 } }); // x - 3

            var p3 = p1 / p2; // x^2 - 9*x - 27 - 123 / (x-3)

            CollectionAssert.AreEqual(
                new Polynomial(new Dictionary<int, double>() { { 2, 1 }, { 1, -9 }, { 0, -27 } }), p3.Item1);

            CollectionAssert.AreEqual(
                new Polynomial(new Dictionary<int, double>() { { 0, -123 } }), p3.Item2);
        }

        [TestCase(new double[] { 7, 3, 1 }, new double[] { -7, -3, -1 }, ExpectedResult = new double[] { 0 })]
        [TestCase(new double[] { 7, 3, 1 }, new double[] { 10, 1 }, ExpectedResult = new double[] { 17, 4, 1 })]
        [TestCase(new double[] { 3, 2 }, new double[] { 1, 4, 10 }, ExpectedResult = new double[] { 4, 6, 10 })]
        [TestCase(new double[] { -5, -10, -1 }, new double[] { -1, -1, -1 }, ExpectedResult = new double[] { -6, -11, -2 })]
        [TestCase(new double[] { 1, 2, 3 }, new double[] { -4, 0, 7 }, ExpectedResult = new double[] { -3, 2, 10 })]
        public double[] AdditionTest(double[] firstPolyCoef, double[] secondPolyCoef)
        {
            var p1 = new Polynomial(firstPolyCoef);
            var p2 = new Polynomial(secondPolyCoef);

            return (p1 + p2).Coefficients;
        }

        [TestCase(new double[] { 10, 5, 3 }, new double[] { -7, -3, -1 }, ExpectedResult = new double[] { 17, 8, 4 })]
        [TestCase(new double[] { 0, 0, 0 }, new double[] { 10, 1 }, ExpectedResult = new double[] { -10, -1 })]
        [TestCase(new double[] { 1, 8 }, new double[] { -1, -4, -10 }, ExpectedResult = new double[] { 2, 12, 10 })]
        public double[] SubtractionTest(double[] firstPolyCoef, double[] secondPolyCoef)
        {
            var p1 = new Polynomial(firstPolyCoef);
            var p2 = new Polynomial(secondPolyCoef);

            return (p1 - p2).Coefficients;
        }

        [TestCase(new double[] { 10, 5, 3, 7, -11 }, 10, ExpectedResult = new double[] { 100, 50, 30, 70, -110 })]
        [TestCase(new double[] { 3, 7, -1 }, -2, ExpectedResult = new double[] { -6, -14, 2 })]
        [TestCase(new double[] { 1, 8 }, 0, ExpectedResult = new double[] { 0 })]
        public double[] MultiplicationByNumberTest(double[] firstPolyCoef, double factor)
        {
            var p1 = new Polynomial(firstPolyCoef);

            return (p1 * factor).Coefficients;
        }

        [TestCase(new double[] { 10, 8, 6, 4, 2, 1 }, 2, ExpectedResult = new double[] { 5, 4, 3, 2, 1, 0.5 })]
        [TestCase(new double[] { 1, -0.6, 0 }, -2, ExpectedResult = new double[] { -0.5, 0.3 })]
        [TestCase(new double[] { 1, 2 }, 1, ExpectedResult = new double[] { 1, 2 })]
        public double[] DivisionByNumberTest(double[] firstPolyCoef, double divisor)
        {
            var p1 = new Polynomial(firstPolyCoef);

            return (p1 / divisor).Coefficients;
        }

        [TestCase(new double[] { 2, 3 }, new double[] { 1, 5 }, ExpectedResult = new double[] { 2, 13, 15 })]
        [TestCase(new double[] { 1, -2, 4 }, new double[] { 3, 0, 7, 9 }, ExpectedResult = new double[] { 3, -6, 19, -5, 10, 36 })]
        [TestCase(new double[] { 2 }, new double[] { 1, 0, 0, 1 }, ExpectedResult = new double[] { 2, 0, 0, 2 })]
        public double[] MultiplicationOfPolynomialsTest(double[] firstPolyCoef, double[] secondPolyCoef)
        {
            var p1 = new Polynomial(firstPolyCoef);
            var p2 = new Polynomial(secondPolyCoef);

            return (p1 * p2).Coefficients;
        }

        [TestCase(new double[] { 2, 3 }, new double[] { 1, 5 }, ExpectedResult = -1)]
        [TestCase(new double[] { 1, -2, 4 }, new double[] { 3, 0, 7, 9 }, ExpectedResult = -1)]
        [TestCase(new double[] { 1, 0, 0, 1 }, new double[] { 10 }, ExpectedResult = 1)]
        [TestCase(new double[] { 1, 2, 3, 4 }, new double[] { 5, 7, 0, 4 }, ExpectedResult = 1)]
        [TestCase(new double[] { 1, 2, 3, 4 }, new double[] { 1, 2, 3, 4 }, ExpectedResult = 0)]
        public int ComparisonOfPolynomialsTest(double[] firstPolyCoef, double[] secondPolyCoef)
        {
            var p1 = new Polynomial(firstPolyCoef);
            var p2 = new Polynomial(secondPolyCoef);

            return p1.CompareTo(p2);
        }

        [Test]
        public void CharacterComparisonOfPolynomialsTest()
        {
            Assert.AreEqual(
                true,
                new Polynomial(new double[] { 1, 2, 3 }) > new Polynomial(new double[] { 1, 2, 1 }));

            Assert.AreEqual(
                true,
                new Polynomial(new double[] { 1, 2, 3 }) >= new Polynomial(new double[] { 1, 2, 1 }));

            Assert.AreEqual(
                true,
                new Polynomial(new double[] { 1, 2, 3 }) >= new Polynomial(new double[] { 1, 2, 3 }));

            Assert.AreEqual(
                false,
                new Polynomial(new double[] { 1, 2, 3 }) > new Polynomial(new double[] { 1, 2, 4 }));

            Assert.AreEqual(
                false,
                new Polynomial(new double[] { 5, 2, 3 }) < new Polynomial(new double[] { 1, 2, 3 }));

            Assert.AreEqual(
                false,
                new Polynomial(new double[] { 5, 2, 3 }) == new Polynomial(new double[] { 1, 3, 3 }));

            Assert.AreEqual(
                true,
                new Polynomial(new double[] { 5, 2, 3 }) != new Polynomial(new double[] { 1, 3, 7 }));
        }
    }
}
