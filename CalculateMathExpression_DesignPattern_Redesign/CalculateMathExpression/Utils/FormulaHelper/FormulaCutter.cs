using CalculateMathExpression.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Utils.FormulaHelper
{
    class FormulaCutter
    {
        List<FormulaElement> supportedElements;
        private FormulaCutter()
        {
            supportedElements = SupportedElement.GetInstance().SupportedElementList;
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

                if (formula[formula.Length - 1] == ']')
                {
                    int i = formula.Length - 1;
                    while (i > 0)
                    {
                        i--;
                        if (formula[i] == '[')
                        {
                            return formula = formula.Substring(0, i);
                        }

                    }
                }
                foreach (FormulaElement e in supportedElements)
                {
                    if (e.ShowForm.Length>=2&&formula.EndsWith(e.ShowForm))
                    {
                        formula = formula.Substring(0, formula.Length - e.ShowForm.Length);
                        return formula;
                    }
                }
             
                formula = formula.Substring(0, formula.Length - 1);

                return formula;
            }
        }
    }
}
