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
           
            if (lastElement.IsCanDelegateLeftNumber && !lastElement.IsCanDelegateRightNumber && toAddElement.IsCanDelegateRightNumber && toAddElement.IsCanDelegateLeftNumber)
            {
                return false;
            }
            if (!toAddElement.IsCanDelegateRightNumber&& !toAddElement.IsCanDelegateLeftNumber)
            {
                if (!lastElement.IsCanDelegateLeftNumber)
                {
                    return false;
                }

            }

            if (lastElement.IsCanDelegateRightNumber&&!lastElement.IsCanDelegateLeftNumber&&toAddElement.IsCanDelegateLeftNumber&& !toAddElement.IsCanDelegateRightNumber)
            {
                return false;
            }

            /*
            // operator can delegate a number follow by a number is not correct;
            if (lastElement.IsCanDelegateRightNumber && !lastElement.IsCanDelegateLeftNumber && toAddElement.IsCanDelegateRightNumber)
            {
                return true;
            }
            // number can follow by number
            if (lastElement.IsCanDelegateLeftNumber && lastElement.IsCanDelegateRightNumber && toAddElement.IsCanDelegateRightNumber && toAddElement.IsCanDelegateLeftNumber)
            {
                return true;
            }
            // number follow by a operator that delegate a number
            if (lastElement.IsCanDelegateLeftNumber && lastElement.IsCanDelegateRightNumber && toAddElement.IsCanDelegateRightNumber)
            {
                return true;
            }
            // number follow by a operator that need left operand
            if (lastElement.IsCanDelegateLeftNumber && lastElement.IsCanDelegateRightNumber && (toAddElement.IsCanDelegateLeftNumber|| (!toAddElement.IsCanDelegateLeftNumber&&!toAddElement.IsCanDelegateRightNumber)))
            {
                return true;
            }
            */
            // same operator was typed.
            if (lastElement.Code == toAddElement.Code)
            {
                if (!lastElement.IsCanDouplicate)
                {
                    return false;
                } 
                
            }
            // operator that need to operand
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
