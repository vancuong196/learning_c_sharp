
namespace CalculateMathExpression.Utils.GrammarValidate
{
    class SubtractionOperatorGrammarValidator : IGrammarValidator
    {
        public bool Validate(string context)
        {
            if (context == null || context == "")
            {
                return true;
            }
            if ("+-*/".Contains(context[context.Length - 1]))
            {
                return false;
            }
            return true;
        }
    }
}
