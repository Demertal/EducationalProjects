using System;

namespace AreaCalculationLibrary
{
    public abstract class Triangle: IFigure
    {
        public double A { get; }

        public double B { get; }

        public double C { get; }

        public int Count { get; }

        protected Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
            Count = 3;
        }

        public abstract double CalculateArea();
    }
}
