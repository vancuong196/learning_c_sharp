
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

            if (context.Contains("²") && context[context.Length - 1] != '²')
            {
                int i = context.Length - 1;
                while (i > 0)
                {
                    if (context[i] == '²')
                    {
                        break;
                    }
                    i--;
                }
                string subGap = context.Substring(i, context.Length - 1 - i);
                if (!(subGap.Contains("*") || subGap.Contains("+") || subGap.Contains("/") || subGap.Contains("-") || subGap.Contains("(") || subGap.Contains(")")))
                {
                    return false;
                }
            }


            if ("+-*/(²".Contains(context[context.Length - 1]))
            {
                return false;
            }
            return true;
        }
    }
}
