using System;

namespace AreaCalculationLibrary
{
    public class TriangleBuilder
    {
        public Triangle Create(double a, double b, double c)
        {
            if (!TriangleExists(a, b, c)) throw new Exception("Треугольник не существует");
            int numRightAngle = FindNumRightAngle(a, b, c);
            if(numRightAngle == 0)
                return  new TriangleSimple(a, b, c);
            else
                return new TriangleRight(a, b, c, numRightAngle);
        }

        public Triangle Create(double a, double b, double c, int numRightAngle)
        {
            return new TriangleRight(a, b, c, numRightAngle);
        }

        //проверка что треугольник существует
        private bool TriangleExists(double a, double b, double c)
        {
            return a > 0 && b > 0 && c > 0 && a + b > c && a + c > b && b + c > a;
        }

        /*проверка что треугольник прямоугольный, если да вернет номер угла = 90*/
        private int FindNumRightAngle(double a, double b, double c)
        {
            double cos1 = (a * a + c * c - b * b) / 2 * a * c;
            if (Math.Abs(cos1) < 0.1) return 1;
            double cos2 = (a * a + b * b - c * c) / 2 * a * b;
            if (Math.Abs(cos2) < 0.1) return 2;
            double cos3 = (c * c + b * b - a * a) / 2 * c * b;
            if (Math.Abs(cos3) < 0.1) return 3;
            return 0;
        }
    }
}
