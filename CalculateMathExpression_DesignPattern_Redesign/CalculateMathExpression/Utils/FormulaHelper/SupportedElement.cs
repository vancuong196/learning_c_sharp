using CalculateMathExpression.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Utils.FormulaHelper
{
    public class SupportedElement
    {
        private static SupportedElement _instance = new SupportedElement();
        private List<FormulaElement> _supportedElements;
        public List<FormulaElement> SupportedElementList
        {
            set
            {
                _supportedElements = value;
            }
            get
            {
                return _supportedElements;
            }
        }
        private SupportedElement()
        {
            _supportedElements = new List<FormulaElement>();
            FormulaElement addOperator = new FormulaElement.FormulaElementBuilder("+", "+", "+").SetIsCanDelegateLeftNumber(false).SetIsCanDelegateRightNumber(false).SetIsCanDouplicate(false).Build();
            _supportedElements.Add(addOperator);
            FormulaElement subOperator = new FormulaElement.FormulaElementBuilder("-", "-", "-").SetIsCanDelegateLeftNumber(false).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(false).Build();
            _supportedElements.Add(subOperator);
            FormulaElement mulOperator = new FormulaElement.FormulaElementBuilder("*", "*", "*").SetIsCanDelegateLeftNumber(false).SetIsCanDelegateRightNumber(false).SetIsCanDouplicate(false).Build();
            _supportedElements.Add(mulOperator);
            FormulaElement divOperator = new FormulaElement.FormulaElementBuilder("/", "/", "/").SetIsCanDelegateLeftNumber(false).SetIsCanDelegateRightNumber(false).SetIsCanDouplicate(false).Build();
            _supportedElements.Add(divOperator);
            FormulaElement openBracket = new FormulaElement.FormulaElementBuilder("(", "(", "(").SetIsCanDelegateLeftNumber(false).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).Build();
            _supportedElements.Add(openBracket);
            FormulaElement closeBracket = new FormulaElement.FormulaElementBuilder(")", ")", ")").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(false).SetIsCanDouplicate(true).Build();
            _supportedElements.Add(closeBracket);
            FormulaElement sqr = new FormulaElement.FormulaElementBuilder("sqr", "²", "^(2)").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(false).SetIsCanDouplicate(false).Build();
            _supportedElements.Add(sqr);
            FormulaElement sqrt = new FormulaElement.FormulaElementBuilder("sqrt", "√(", "sqrt(").SetIsCanDelegateLeftNumber(false).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(false).Build();
            _supportedElements.Add(sqrt);
            FormulaElement variableClose = new FormulaElement.FormulaElementBuilder("]", "]", "]").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(false).SetIsCanDouplicate(false).Build();
            _supportedElements.Add(variableClose);
        
        }
        public static SupportedElement GetInstance()
        {
            return _instance;
        }
        public void AddSupportElement(FormulaElement element)
        {
            if (element!=null && _supportedElements != null)
            {
                SupportedElementList.Add(element);
            }
        }
        public bool IsANumberElement(string code)
        {
            return code==null?false:"0123456789".Contains(code) && code.Length == 1;
        }
        public FormulaElement GetNumberElementByCode(string code)
        {
            if (!IsANumberElement(code))
            {
                return null;
            } else
            {
                return new FormulaElement.FormulaElementBuilder(code, code, code).SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).Build();

            }
        }
        public bool IsAVariableElement(string code)
        {
            return code == null ? false :code.Length>=2 &&code[0]=='[' && code[code.Length-1] == ']';
        }
        public FormulaElement GetNumberVariableByCode(string code)
        {
            if (!IsAVariableElement(code))
            {
                return null;
            }
            else
            {
                return new FormulaElement.FormulaElementBuilder(code, code, code).SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).SetIsAvariable(true).Build();

            }
        }
        public bool IsElementSupported(string code)
        {

            if (code == null|| code =="" || _supportedElements==null)
            {
                return false;
            }
            foreach (FormulaElement e in _supportedElements)
            {
                if (code == e.Code|| e.ShowForm.Contains(code))
                {
                    return true;
                }
            }
            return false;
        }
        public FormulaElement GetElementByCode(string code)
        {
            if (IsANumberElement(code))
            {
                return GetNumberElementByCode(code);
            }
            if (IsAVariableElement(code))
            {
                return GetNumberVariableByCode(code);
            }
            if (!IsElementSupported(code))
            {
                return null;
            }
            else return _supportedElements.First(s => {
                if (s.Code.EndsWith(code))
                {
                    return true;
                }
                if (s.ShowForm.EndsWith(code))
                {
                    return true;
                }
                return false;
                
                });
        }
       

    }
}
