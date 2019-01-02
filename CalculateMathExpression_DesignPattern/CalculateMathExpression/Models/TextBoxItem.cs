using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Models
{
    public class TextboxItem
    {
        public String Value { set; get; }
        public String Name { set; get; }
        public TextboxItem(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
