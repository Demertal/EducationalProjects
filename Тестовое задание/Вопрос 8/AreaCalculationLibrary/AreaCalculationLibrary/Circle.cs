using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaCalculationLibrary
{
    public class Circle: IFigure 
    {
        public int Count { get; }

        private double _r;
        public double R
        {
            get => _r;
            set => _r = value;
        }

        public Circle()
        {
            Count = 0;
        }

        public Circle(double r)
        {
            Count = 0;
            R = r;
        }

        public double CalculateArea()
        {
            return Math.PI * R * R;
        }
    }
}
