using CalculateMathExpression.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Utils
{
   class LogService : IInfomationService, MainPageDataChangedListener
    {
        public void ShowMessgae(string message)
        {
            DateTime currentTime = DateTime.Now;
            Debug.WriteLine("Calculator Application ("+currentTime.ToString("dd/MM/yyyy HH:mm:ss")+"): " +message);
        }
        private LogService()
        {

        }
        private static class LogMessageHelper
        {
            public static LogService Instance = new LogService();
        }
        public static LogService GetLogger()
        {
            return LogMessageHelper.Instance;
        }

        public void OnSavedFormula(string formulaX, string formulaY)
        {
            ShowMessgae("Saved formulas! XFormula=" + formulaX + " YFormula=" + formulaY);
            
        }
    }
}
