using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaCalculationLibrary
{
    public interface IFigure : IAreaCalculation
    {
        /// <summary>
        /// Кол-во сторон
        /// </summary>
        int Count { get; }
    }
}
