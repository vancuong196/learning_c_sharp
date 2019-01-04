
namespace CalculateMathExpression.Utils.GrammarValidate
{
    class GrammarValidatorFactory
    {
        public static IGrammarValidator GetGrammarValidator(string type)
        {
            if (type.Contains("["))
            {
                return new VariableGrammarVailidator(new NumberGrammarValidator());
            }
            else if (type == "sqr")
            {
                return new SqrGrammarValidator();
            }
            else if ("/" == type)
            {
                return new DivisionOperatorGrammarValidator();
            }
            else if ("*" == type)
            {
                return new MultiplicationOperatorGrammarValidator();
            }
            else if (")".Contains(type))
            {
                return new ClosingBracketGrammarValidator();
            }
            else if ("0123456789".Contains(type))
            {
                return new NumberGrammarValidator();
            }
            else if (type == "-")
            {
                return new SubtractionOperatorGrammarValidator();
            }
            else if (type == "+")
            {
                return new AdditionOperatorGrammarValidator();
            }
            return new TrueGrammarValidator();
        }
        public static IGrammarValidator GetSentenceGrammarValidator()
        {
            return new SentenceGrammarValidator();
        }
    }
}
