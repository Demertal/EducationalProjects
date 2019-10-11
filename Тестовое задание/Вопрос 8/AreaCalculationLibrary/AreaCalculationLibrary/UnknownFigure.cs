using System;
using System.Collections.Generic;

namespace AreaCalculationLibrary
{
    public class UnknownFigure: IFigure
    {
        public int Count { get; }

        //Список известных переменных, где в качестве ключа задается общепринятое обозначение
        public Dictionary<string, double> Parameters { get; set; }
        
        public UnknownFigure(int count)
        {
            Count = count;
        }

        public UnknownFigure(int count, Dictionary<string, double> parameters)
        {
            Count = count;
            Parameters = parameters;
        }

        public double CalculateArea()
        {
            switch (Count)
            {
                case 3:
                    IFigure triangle = new TriangleBuilder().Create(Parameters["a1"], Parameters["a2"], Parameters["a3"]);
                    return triangle.CalculateArea();
                case 0:
                    IFigure circle = new Circle(Parameters["r"]);
                    return circle.CalculateArea();
            }

            return 0;
        }
    }
}
