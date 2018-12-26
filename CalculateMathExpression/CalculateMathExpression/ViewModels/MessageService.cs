using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace CalculateMathExpression.ViewModels
{
    class MessageService : IMessageService
    {
        public void ShowMessgae(string message)
        {
            var dialog = new MessageDialog(message);
            dialog.Title = "Info";
            dialog.ShowAsync();
        }
    }
}
