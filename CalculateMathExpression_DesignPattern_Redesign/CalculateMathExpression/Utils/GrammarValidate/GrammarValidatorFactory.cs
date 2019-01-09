
namespace CalculateMathExpression.Utils.GrammarValidate
{
    class GrammarValidatorFactory
    {
        public static ILastElementGrammarValidator GetLastElementValidator(string context)
        {

            return new LastElementGrammarValidatorDoubleNumberSupported(new LastElementValidator(), context);
        }
        public static IGrammarSentenceValidator GetSentenceGrammarValidator()
        {
            return new SentenceGrammarValidator();
        }
    }
}
