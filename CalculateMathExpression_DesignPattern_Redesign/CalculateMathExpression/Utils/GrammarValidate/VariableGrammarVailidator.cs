
namespace CalculateMathExpression.Utils.GrammarValidate
{
   
    class VariableGrammarVailidator : IGrammarValidator
    {
        private IGrammarValidator _validator;
        public VariableGrammarVailidator(IGrammarValidator validate)
        {
            _validator = validate;
        }
        public bool Validate(string context)
        {
            if (_validator.Validate(context))
            {
                if (!(context.Length >= 1 && "0123456789".Contains(context[context.Length - 1])))
                {
                   // LogService.GetLogger().ShowMessgae("true");
                    return true;                                                                                                                                                        
                    
                }                                                                                                                                                                                                                                                                                                                                                      
            }
         //   LogService.GetLogger().ShowMessgae("false");
            return false;

        }
    }
}
