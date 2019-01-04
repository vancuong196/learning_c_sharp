
namespace CalculateMathExpression.Utils.GrammarValidate
{
    class NumberGrammarValidator : IGrammarValidator
    {
        public bool Validate(string context)
        {
            if (context == "")
            {
                return true;
            }

            if (")]²".Contains(context[context.Length - 1]))
            {
                return false;
            }
            return true;
        }

    }
}
