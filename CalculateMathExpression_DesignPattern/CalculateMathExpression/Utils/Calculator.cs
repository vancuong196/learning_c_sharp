using info.lundin.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Utils
{
    class Calculator : ICalculator
    {

        public double Calculate(string expression)
        {

            try
            {
                ExpressionParser parser = new ExpressionParser();
                return parser.Parse(expression);
            } catch
            {
                throw new Exception();
            }
           
        }
    }
}
