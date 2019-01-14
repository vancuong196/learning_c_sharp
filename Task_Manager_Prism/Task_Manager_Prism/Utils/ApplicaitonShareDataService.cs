using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_Prism.Models;

namespace Task_Manager_Prism.Utils
{
    class ApplicaitonShareDataService
    {
        
        private ApplicaitonShareDataService()
        {
        }
        private class Helper
        {
            public static ApplicaitonShareDataService _instance = new ApplicaitonShareDataService();
        }
        public static ApplicaitonShareDataService GetCurrent()
        {
            return Helper._instance;
        }
        public TokenModel Token { set; get; }
       
    }
}
