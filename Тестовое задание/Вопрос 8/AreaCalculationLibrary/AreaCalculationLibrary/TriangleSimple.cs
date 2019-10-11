using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaCalculationLibrary
{
    public class TriangleSimple: Triangle
    {
        public TriangleSimple(double a, double b, double c) : base(a, b, c)
        {
        }

        public override double CalculateArea()
        {
            double p = (A + B + C) / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }
    }
}
