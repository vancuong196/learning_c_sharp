
namespace CalculateMathExpression.Utils.GrammarValidate
{
    class GrammarValidatorFactory
    {
        public static ILastElementGrammarValidator GetLastElementValidator()
        {

            return new LastElementGrammarValidatorDoubleNumberSupported(new LastElementValidator());
        }
        public static IGrammarSentenceValidator GetSentenceGrammarValidator()
        {
            return new SentenceGrammarValidator();
        }
    }
}
