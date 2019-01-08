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
        private bool ValidateDot(string context)
        {

            if (!context.Contains('.'))
            {
                return true;
            }
            for (int i= 0; i < context.Length-1; i++)
            {
                if (context[i] == '.')
                {
                    bool isFault = true;
                    int j = i + 1;
                    while (j<context.Length-1&&context[j]!='.')
                    {
                        if (!"0123456789".Contains(context[j]))
                        {
                            isFault = false;
                        }
                        j++;
                    }
                    if (j >= context.Length)
                    {
                        isFault = false;
                    }
                    if (isFault)
                    {
                        return false;
                    }
                }
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
            if (!ValidateDot(context))
            {
                throw new Exception("Error! Float number with two or more dot detected: " + context);
            }

            return true;
        }
    }
}
