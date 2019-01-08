using CalculateMathExpression.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Utils.GrammarValidate
{
    class LastElementValidator : ILastElementGrammarValidator
    {
        public bool Validate(FormulaElement lastElement, FormulaElement toAddElement)
        {
            // not supported element typed!
            if (toAddElement == null)
            {
                return false;
            }
            // formula is empty
            if (lastElement == null)
            {
                if (toAddElement.IsCanDelegateRightNumber)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (lastElement.Code == toAddElement.Code)
            {
                // element follow by the same element is fault. (ex: ++ --)
                if (!lastElement.IsCanDouplicate)
                {
                    return false;
                }

            }
            if (lastElement.IsCanDelegateLeftNumber && !lastElement.IsCanDelegateRightNumber && toAddElement.IsCanDelegateRightNumber && toAddElement.IsCanDelegateLeftNumber)
            {
                return false;
            }
            // check +-*/ (operator that need two operand)
            if (!toAddElement.IsCanDelegateRightNumber&& !toAddElement.IsCanDelegateLeftNumber)
            {
                //operator that need two operand can be only added behind a number
                if (!lastElement.IsCanDelegateLeftNumber)
                {
                    return false;
                }

            }
            // element that delegate a number in left side can not follow by an element that delegate a number in right side. (ex '()' is fault ) 
            if (lastElement.IsCanDelegateRightNumber&&!lastElement.IsCanDelegateLeftNumber&&toAddElement.IsCanDelegateLeftNumber&& !toAddElement.IsCanDelegateRightNumber)
            {
                return false;
            }
            // mumber follow by a variable is fault (ex 8[XC1])
            if (lastElement.IsCanDelegateRightNumber && lastElement.IsCanDelegateLeftNumber && toAddElement.IsAVariable)
            {
                return false;
            }


            // operator that need two operand need second operand to be a number.
            if (!lastElement.IsCanDelegateRightNumber && !lastElement.IsCanDelegateLeftNumber)
            {
                if (!toAddElement.IsCanDelegateRightNumber)
                {
                    return false;
                }
            
            }
           
            return true;
        }
      
       
    }
}
