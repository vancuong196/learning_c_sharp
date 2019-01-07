using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//TODO
namespace CalculateMathExpression.Utils.Calculator
{
    class MyCalculator : ICalculator
    {
        public double Calculate(string expression)
        {
            throw new NotImplementedException();
        }
        private string ReplaceSquareRoot(string expression)
        {
            if (!expression.Contains("^(2)"))
            {
                return expression;
            }
            else 
            {
                for (int i=expression.Length-1;i>0; i--)
                {
                    if (expression[i] == '^')
                    {
                        for (int j = i-1; j >=0 ; j--)
                        {
                            if ("+-*/".Contains(expression[j]))
                            {
                                string simple = expression.Substring(j+1, i+ 3 -j + 1);
                                expression.Replace(simple, simple.Split('^')[0] + "*" + simple.Split('^')[0]);
                               
                                break;
                            }
                        }
                        break;
                    }
                }
                return ReplaceSquareRoot(expression);
            }
        }
        private string FindFirstSimpleExpression(string expression)
        {
            if (!expression.Contains("("))
            {
                return expression;
            }
            string temp = expression;
            temp = temp.Replace("^(2)", "");
            if (!temp.Contains("("))
            {
                return expression;
            }
            string simpleExpression = "";
            for (int i = 0; i< expression.Length; i++)
            {
                if (expression[i] == '(')
                {
                    if (i-1>0&& expression[i - 1] != '^')
                    {
                        continue;
                    }
                    else
                    {
                        for (int j = i + 1; j < expression.Length;j++)
                        {
                            if (expression[j] == ')')
                            {
                                simpleExpression = expression.Substring(i, j - i + 1);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return simpleExpression;
        }
        public string ExpressionCalculate(string expression)
        {
            do
            {
                string simpleExpression = FindFirstSimpleExpression(expression);
                string tempExpression = simpleExpression;
                tempExpression = ReplaceSquareRoot(tempExpression);



            } while (true);
        }
        public string SimpleExpressionCalculate(string expression)
        {
             if (expression[0] == '1')
            {
                expression = "0" + expression;
            }
             if (!(expression.Contains('+')|| expression.Contains('-')|| expression.Contains('*')|| expression.Contains('/')))
            {
                return expression;
            }
            expression.Replace("+", " + ");
            expression.Replace("-", " - ");
            expression.Replace("/", " / ");
            expression.Replace("*", " * ");
            string [] elementsOfExpression = expression.Split(" ");
            bool ishaveMulOrDiv = true;
            do
            {

                for (int i = 0; i < elementsOfExpression.Length; i++)
                {
                    if (elementsOfExpression[i] == "*")
                    {
                        double a = 0;
                        Double.TryParse(elementsOfExpression[i - 1], out a);
                        double b = 0;
                        Double.TryParse(elementsOfExpression[i - 1], out b);
                        double c = a * b;
                        ishaveMulOrDiv = true;
                        break;
                    }
                }
            } while (true);

        }
    }
}
