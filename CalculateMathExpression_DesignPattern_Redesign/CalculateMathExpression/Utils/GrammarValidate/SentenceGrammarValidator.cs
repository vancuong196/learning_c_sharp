using CalculateMathExpression.Models;
using CalculateMathExpression.Utils.FormulaHelper;
using System;


namespace CalculateMathExpression.Utils.GrammarValidate
{
    class SentenceGrammarValidator : IGrammarSentenceValidator
    {
        private bool ValidateEnding(string context)
        {
           
                if (context != "" && context != null)
                {
                FormulaElement lastElement = SupportedElement.GetInstance().GetElementByCode(context[context.Length - 1].ToString());
                if (lastElement == null)
                {
                    return false;
                }
                if (!lastElement.IsCanDelegateLeftNumber)
                {
                    return false;
                }
                }
                return true;
        

        }
        private bool ValidateBracket(string context)
        {
            int count = 0;

            foreach (char c in context)
            {
                if (c == '(')
                {
                    count++;
                }
                if (c == ')')
                {
                    count--;
                }
            }
            if (count != 0)
            {
                return false;
                
            }


            return true;


        }

        public bool Validate(string context)
        {
            if (!ValidateEnding(context))
            {
                throw new System.Exception(context + " is not correct! (End with '" + context[context.Length - 1] + "').");
                
            }
            if (!ValidateBracket(context))
            {
                throw new Exception("Error! Bracket fault in Formula: " + context);
            }

            return true;
        }
    }
}
