using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager_Prism.Utils
{
    interface IRegisterService
    {
        Task<bool> RegisterAsync(string username, string password, string confirmPassword);
        string GetError();
    }
}
