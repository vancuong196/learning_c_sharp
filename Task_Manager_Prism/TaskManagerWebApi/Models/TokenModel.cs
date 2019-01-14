using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagerWebApi.Models
{
    
    public class TokenModel
    {
        public string access_token { set; get; }
        public string token_type { set; get; }
        public int expires_in { set; get; }
    }
}