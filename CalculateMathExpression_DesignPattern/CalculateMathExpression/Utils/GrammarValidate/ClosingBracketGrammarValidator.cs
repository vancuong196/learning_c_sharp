
namespace CalculateMathExpression.Utils.GrammarValidate
{
    class ClosingBracketGrammarValidator : IGrammarValidator
    {
        public bool Validate(string context)
        {
            if (context == "" || context == null)
            {
                return false;
            }
            int count = 0;
            if ("+-*/(".Contains(context[context.Length - 1]))
            {
                return false;
            }
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
            if (count <= 0)
            {
                return false;
            }
            return true;

        }
    }
}
