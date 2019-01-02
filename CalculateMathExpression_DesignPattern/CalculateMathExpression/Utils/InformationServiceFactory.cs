using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Utils
{
    public class InformationServiceFactory
    {
        public IInfomationService GetInformationService(string type)
        {
            if (type == typeof(LogService).Name)
            {
                return LogService.GetLogger();
            }
            else if (type == typeof(MessageService).Name)
            {
                return new MessageService();
            }
            else
            {
                return new NullMessageService();
            }
            
        }
    }
}
