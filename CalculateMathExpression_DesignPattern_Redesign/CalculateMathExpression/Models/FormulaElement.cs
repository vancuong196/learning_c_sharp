using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Models
{
    public class FormulaElement
    {
        public String Code { set; get; }
        public string ShowForm { set; get; }
        public string MathForm { set; get; }
        public bool IsCanDouplicate { set; get; }
        public bool IsCanDelegateLeftNumber { set; get; }
        public bool IsCanDelegateRightNumber { set; get; }
      
        private FormulaElement(FormulaElementBuilder builder)
        {
            Code = builder.Code;
            MathForm = builder.MathForm;
            ShowForm = builder.ShowForm;
            IsCanDelegateRightNumber = builder.IsCanDelegateRightNumber;
            IsCanDelegateLeftNumber = builder.IsCanDelegateLeftNumber;
            IsCanDouplicate = builder.IsCanDouplicate;

        }
        public class FormulaElementBuilder
        {
            public String Code { set; get; }
            public string ShowForm { set; get; }
            public string MathForm { set; get; }
            public bool IsCanDouplicate { set; get; }
            public bool IsCanDelegateLeftNumber { set; get; }
            public bool IsCanDelegateRightNumber { set; get; }
            public FormulaElementBuilder(string code, string showForm, string mathForm)
            {
                Code = code;
                MathForm = mathForm;
                ShowForm = showForm;
            }
            public FormulaElementBuilder SetIsCanDouplicate(bool isCanDouplicate)
            {
                IsCanDouplicate = isCanDouplicate;
                return this;
            }
            public FormulaElementBuilder SetIsCanDelegateLeftNumber(bool isCanDelegateLeftNumber)
            {
                IsCanDelegateLeftNumber = isCanDelegateLeftNumber;
                return this;
            }
            public FormulaElementBuilder SetIsCanDelegateRightNumber(bool isCanDelegateRightNumber)
            {
                IsCanDelegateRightNumber = isCanDelegateRightNumber;
                return this;
            }
            public FormulaElement Build()
            {
                return new FormulaElement(this);
            }
        }
            
    }
}
