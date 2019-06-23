using TasksLibrary;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{

    [TestFixture]
    public class PolynomialTests
    {
        [TestCase(new double[] { 7, 3, 1 }, new double[] { 10 }, ExpectedResult = new double[] { 17, 3, 1 })]
        [TestCase(new double[] { 3 }, new double[] { 1, 4, 10 }, ExpectedResult = new double[] { 4, 4, 10 })]
        [TestCase(new double[] { -5, -10, -1 }, new double[] { -1, -1, -1 }, ExpectedResult = new double[] { -3, 2, 10 })]
        [TestCase(new double[] { 1, 2, 3 }, new double[] { -4, 0, 7 }, ExpectedResult = new double[] { -3, 2, 10 })]
        public double[] AdditionTest(double[] firstPolyCoef, double[] secondPolyCoef)
        {
            var p1 = new Polynomial(firstPolyCoef);
            var p2 = new Polynomial(secondPolyCoef);

            return (p1 + p2).Coefficients;
        }
    }
}
