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
        public LastElementGrammarValidatorDoubleNumberSupported(ILastElementGrammarValidator validator)
        {
            _basicValidator = validator;
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
