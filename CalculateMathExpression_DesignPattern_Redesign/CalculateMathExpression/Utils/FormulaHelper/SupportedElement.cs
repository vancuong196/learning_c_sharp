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
            FormulaElement subOperator = new FormulaElement.FormulaElementBuilder("-", "-", "-").SetIsCanDelegateLeftNumber(false).SetIsCanDelegateRightNumber(false).SetIsCanDouplicate(false).Build();
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
            FormulaElement zero = new FormulaElement.FormulaElementBuilder("0", "0", "0").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).Build();
            _supportedElements.Add(zero);
            FormulaElement one = new FormulaElement.FormulaElementBuilder("1", "1", "1").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).Build();
            _supportedElements.Add(one);
            FormulaElement two = new FormulaElement.FormulaElementBuilder("2", "2", "2").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).Build();
            _supportedElements.Add(two);
            FormulaElement three = new FormulaElement.FormulaElementBuilder("3", "3", "3").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).Build();
            _supportedElements.Add(three);
            FormulaElement four = new FormulaElement.FormulaElementBuilder("4", "4", "4").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).Build();
            _supportedElements.Add(four);
            FormulaElement five = new FormulaElement.FormulaElementBuilder("5", "5", "5").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).Build();
            _supportedElements.Add(five);
            FormulaElement six = new FormulaElement.FormulaElementBuilder("6", "6", "6").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).Build();
            _supportedElements.Add(six);
            FormulaElement seven = new FormulaElement.FormulaElementBuilder("7", "7", "7").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).Build();
            _supportedElements.Add(seven);
            FormulaElement eight = new FormulaElement.FormulaElementBuilder("8", "8", "8").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).Build();
            _supportedElements.Add(eight);
            FormulaElement nine = new FormulaElement.FormulaElementBuilder("9", "9", "9").SetIsCanDelegateLeftNumber(true).SetIsCanDelegateRightNumber(true).SetIsCanDouplicate(true).Build();
            _supportedElements.Add(nine);
   


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
        public bool IsElementSupported(string code)
        {
            if (code == null|| code =="" || _supportedElements==null)
            {
                return false;
            }
            foreach (FormulaElement e in _supportedElements)
            {
                if (code == e.Code)
                {
                    return true;
                }
            }
            return false;
        }
        public FormulaElement GetElementByCode(string code)
        {
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
