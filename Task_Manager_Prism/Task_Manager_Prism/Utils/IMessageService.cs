using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager_Prism.Utils
{
    interface IMessageService
    {
        void ShowMessage(string messsage);
        void ShowMessage(string title, string message);
    }
}
