
namespace CalculateMathExpression.Utils.GrammarValidate
{
    class MultiplicationOperatorGrammarValidator : IGrammarValidator
    {
        public bool Validate(string context)
        {
            if (context == "" || context == null)
            {
                return false;
            }
            int count = 0;
            if ("-+*/(".Contains(context[context.Length - 1]))
            {
                return false;
            }
            return true;
        }
    }
}
