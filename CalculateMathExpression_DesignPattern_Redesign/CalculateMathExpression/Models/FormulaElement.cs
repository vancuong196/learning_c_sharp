using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Models
{
    public class FormulaElement
    {
        public String Code { get; }
        public string ShowForm { get; }
        public string MathForm { get; }
        public bool IsCanDouplicate { get; }
        public bool IsCanDelegateLeftNumber { get; }
        public bool IsCanDelegateRightNumber {get; }
        public bool IsAVariable { get; }
        public bool IsADot { get; }
        private FormulaElement(FormulaElementBuilder builder)
        {
            Code = builder.Code;
            MathForm = builder.MathForm;
            ShowForm = builder.ShowForm;
            IsCanDelegateRightNumber = builder.IsCanDelegateRightNumber;
            IsCanDelegateLeftNumber = builder.IsCanDelegateLeftNumber;
            IsCanDouplicate = builder.IsCanDouplicate;
            IsAVariable = builder.IsAVariable;
            IsADot = builder.IsDot;

        }
        public class FormulaElementBuilder
        {
            public String Code { set; get; }
            public string ShowForm { set; get; }
            public string MathForm { set; get; }
            public bool IsCanDouplicate { set; get; }
            public bool IsCanDelegateLeftNumber { set; get; }
            public bool IsCanDelegateRightNumber { set; get; }
            //special element that need to have describe.
            public bool IsAVariable { set; get; }
            public bool IsDot { set; get; }
            public FormulaElementBuilder(string code, string showForm, string mathForm)
            {
                Code = code;
                MathForm = mathForm;
                ShowForm = showForm;
                IsAVariable = false;
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
            public FormulaElementBuilder SetIsAvariable(bool isVariable)
            {
                IsAVariable = isVariable;
                return this;
            }
            public FormulaElementBuilder SetIsCanDelegateRightNumber(bool isCanDelegateRightNumber)
            {
                IsCanDelegateRightNumber = isCanDelegateRightNumber;
                return this;
            }
            public FormulaElementBuilder SetIsADot(bool isADot)
            {
                IsDot = isADot;
                if (isADot)
                {
                    IsAVariable = false;
                }
                return this;
            }
            public FormulaElement Build()
            {
                return new FormulaElement(this);
            }
        }
            
    }
}
