using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Utils.FormulaHelper
{
    class FormulaCutter
    {
        private FormulaCutter()
        {

        }
        private static class FormulaCutterHolder
        {
            public static FormulaCutter Instance = new FormulaCutter();
        }
        public static FormulaCutter GetInstance()
        {
            return FormulaCutterHolder.Instance;
        }
        public string CutLastElement(string formula)
        {
            if (formula == "")
            {
                return formula;
            }
            else
            {
                if (formula.Length >= 2)
                {
                    if (formula[formula.Length - 1] == '(' && formula[formula.Length - 2] == '√')
                    {
                        formula = formula.Substring(0, formula.Length - 2);
                        return formula;
                    }

                }
                if (formula[formula.Length - 1] == ']')
                {
                    int i = formula.Length - 1;
                    while (i > 0)
                    {
                        i--;
                        if (formula[i] == '[')
                        {
                            formula = formula.Substring(0, i);
                            break;
                           
                        }

                    }
                }
                else
                {
                    formula = formula.Substring(0, formula.Length - 1);
                    
                }
                return formula;
            }
        }
    }
}
