
namespace CalculateMathExpression.Utils.GrammarValidate
{
    class GrammarValidatorFactory
    {
        public static ILastElementGrammarValidator GetLastElementValidator()
        {

            return new LastElementValidator();
        }
        public static IGrammarSentenceValidator GetSentenceGrammarValidator()
        {
            return new SentenceGrammarValidator();
        }
    }
}
