using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Task_Manager_Prism.Utils
{
    class MessageService : IMessageService
    {
        public async void ShowMessage(string message)
        {
            MessageDialog messageBox = new MessageDialog(message);
            await messageBox.ShowAsync();
             
        }

        public async void ShowMessage(string title, string message)
        {
            MessageDialog messageBox = new MessageDialog(message);
            messageBox.Title = title;
            await messageBox.ShowAsync();
        }
    }
}
