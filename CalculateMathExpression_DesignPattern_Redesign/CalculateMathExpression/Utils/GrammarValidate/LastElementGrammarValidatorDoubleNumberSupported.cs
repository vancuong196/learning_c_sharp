using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculateMathExpression.Models;

namespace CalculateMathExpression.Utils.GrammarValidate
{
    class LastElementGrammarValidatorDoubleNumberSupported : ILastElementGrammarValidator
    {
        private ILastElementGrammarValidator _basicValidator;
        private string _sentence;
        public LastElementGrammarValidatorDoubleNumberSupported(ILastElementGrammarValidator validator, string sentence)
        {
            _basicValidator = validator;
            _sentence = sentence;
        }
        private bool ValidateDot(string context)
        {

            if (!context.Contains('.'))
            {
                return true;
            }
            bool isFault = true;
            int j = context.Length - 1;
            while (j >= 0 && context[j] != '.')
            {
                if (!"0123456789".Contains(context[j]))
                {
                    isFault = false;
                    break;
                }
                j--;
            }
            if (j < 0)
            {
                isFault = false;
            }
            if (isFault)
            {
                return false;
            }
            return true;

        }

    
        public bool Validate(FormulaElement lastElement, FormulaElement toAddElement)
        {
            if (lastElement != null && toAddElement != null)
            {
                // code support for a dot
                if (toAddElement.IsADot)
                {
                    if (lastElement.IsAVariable)
                    {
                        return false;
                    }
                    if (!(lastElement.IsCanDelegateLeftNumber && lastElement.IsCanDelegateRightNumber))
                    {
                        return false;
                    }
                    if (!ValidateDot(_sentence))
                    {
                        return false;
                    }
                }
                if (lastElement.IsADot)
                {
                    if (!(toAddElement.IsCanDelegateLeftNumber && toAddElement.IsCanDelegateRightNumber && !toAddElement.IsAVariable))
                    {
                        return false;
                    }
                }


            }
            return _basicValidator.Validate(lastElement, toAddElement);
        }
        
      
    }
}
