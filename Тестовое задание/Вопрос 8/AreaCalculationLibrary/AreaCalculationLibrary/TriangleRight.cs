using System;

namespace AreaCalculationLibrary
{
    public class TriangleRight: Triangle
    {
        public int NumRightAngle { get; }

        public TriangleRight(double a, double b, double c, int numRightAngle) : base(a, b, c)
        {
            NumRightAngle = numRightAngle;
        }

        public override double CalculateArea()
        {
            switch (NumRightAngle)
            {
                case 1:
                    return 0.5 * B * C;
                case 2:
                    return 0.5 * B * A;
                case 3:
                    return 0.5 * C * A;
            }

            return 0;
        }
    }
}
