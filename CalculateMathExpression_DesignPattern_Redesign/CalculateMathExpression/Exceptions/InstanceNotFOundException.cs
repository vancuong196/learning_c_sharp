using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Exceptions
{
    class InstanceNotFoundException: Exception
    {
        public InstanceNotFoundException(string message): base(message)
        {
           
        }
    }
}
