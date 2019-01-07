using CalculateMathExpression.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Utils.GrammarValidate
{
    interface ILastElementGrammarValidator
    {
        bool Validate(FormulaElement lastElement, FormulaElement toAddElement);
    }
}
