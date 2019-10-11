using System.Collections.Generic;
using AreaCalculationLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Test—ircleAreaCalculation()
        {
            IFigure circle = new Circle(4);

            double area = circle.CalculateArea();

            Assert.AreEqual(area, 50.24, 0.1);
        }


        [DataTestMethod]
        [DataRow(4, 5, 6, 9.92)]
        [DataRow(3, 3, 4.242, 4.5)]
        public void TestTriangleAreaCalculation(double a, double b, double c, double result)
        {
            Triangle triangle = new TriangleBuilder().Create(a, b, c);

            double area = triangle.CalculateArea();
            Assert.AreEqual(area, result, 0.1);
        }

        [TestMethod]
        public void TestUnknownFigure()
        {
            IFigure unknownFigure = new UnknownFigure(0, new Dictionary<string, double>{{"r", 4}});

            double area = unknownFigure.CalculateArea();

            Assert.AreEqual(area, 50.24, 0.1);
        }
    }
}
