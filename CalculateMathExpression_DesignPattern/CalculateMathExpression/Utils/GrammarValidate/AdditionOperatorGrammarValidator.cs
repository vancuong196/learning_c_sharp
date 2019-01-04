
namespace CalculateMathExpression.Utils.GrammarValidate
{
    class AdditionOperatorGrammarValidator : IGrammarValidator
    {
        public bool Validate(string contex)
        {
            if (contex == "")
            {
                return false;
            }
            if ("+-*/(".Contains(contex[contex.Length - 1]))
            {
                return false;
            }
            return true;
        }
    }
}
