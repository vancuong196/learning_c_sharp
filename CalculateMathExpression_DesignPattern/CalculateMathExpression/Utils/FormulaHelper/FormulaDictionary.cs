using CalculateMathExpression.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Utils
{
    public class FormulaDictionary
    {
        private Dictionary<string, string> _symbolsDictionary;
        private FormulaVariable[] _knownVariableList;
        public FormulaDictionary(FormulaVariable[] variablesList)
        {
            _knownVariableList = variablesList;
            _symbolsDictionary = new Dictionary<string, string>();
            _symbolsDictionary.Add("√", "sqrt");
            _symbolsDictionary.Add("²", "^(2)");

        }
        private string ReplaceSymbol(string formula)
        {

            foreach (string key in _symbolsDictionary.Keys)
            {
                string text = "";
                _symbolsDictionary.TryGetValue(key, out text);
                formula = formula.Replace(key, text);
            }
            return formula;
        }
        private string ReplaceVariable(string formula)
        {
            var neededVariable = _knownVariableList.Where(s => formula.Contains(s.Name) || formula.Contains(s.Name));

            foreach (FormulaVariable textBox in neededVariable)
            {
                if (textBox.Value == "")
                {
                    throw new Exception("Error! You must input a value for: " + textBox.Name + "!");
                }
                else
                {
                    double valueOfTextbox;
                    if (!Double.TryParse(textBox.Value, out valueOfTextbox))
                    {
                        throw new Exception("Error! Textbox: " + textBox.Name + " does not contain a number!");
                    }
                    formula = formula.Replace((string)textBox.Name, valueOfTextbox.ToString());

                }
            }
            return formula;
        }

        public string TryTranslateToMathEpression(string formula)
        {
            formula = ReplaceSymbol(formula);
            try
            {
                formula = ReplaceVariable(formula);
                return formula;
            } catch ( Exception e)
            {
                throw e;
            }
        }

    }
}
