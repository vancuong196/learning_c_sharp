using info.lundin.math;
using System;

namespace CalculateMathExpression.Utils
{
    class ThirdPartyCalculator : ICalculator
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
