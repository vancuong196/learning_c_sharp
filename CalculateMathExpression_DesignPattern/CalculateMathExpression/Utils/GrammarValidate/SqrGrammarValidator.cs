﻿
namespace CalculateMathExpression.Utils.GrammarValidate
{
    class SqrGrammarValidator : IGrammarValidator
    {
        public bool Validate(string context)
        {
            if (context == "" || context == null)
            {
                return false;
            }

            if ("+-*/(²".Contains(context[context.Length - 1]))
            {
                return false;
            }
            return true;
        }
    }
}
