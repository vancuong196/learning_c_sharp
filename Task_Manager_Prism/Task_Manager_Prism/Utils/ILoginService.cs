using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_Prism.Models;

namespace Task_Manager_Prism.Utils
{
    interface ILoginService
    {
        Task<bool> Login(string userName, string password);
        TokenModel GetToken();
    }
}
