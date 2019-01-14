using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_Prism.Models;
using Task_Manager_Prism.Ultils;

namespace Task_Manager_Prism.Utils
{
    class LoginService : ILoginService
    {
        TokenModel _tokenModel;
        public TokenModel GetToken()
        {
            return _tokenModel;
        }


        async Task<bool> ILoginService.Login(string userName, string password)
        {
            Task<TokenModel> t = GetToken(Constants.LoginBaseUrl, userName, password);
            t.Wait();
            _tokenModel = t.Result;
            if (_tokenModel != null)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
        private async Task<TokenModel> GetToken(string url, string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", userName ),
                        new KeyValuePair<string, string> ( "Password", password )
                    };
            var content = new FormUrlEncodedContent(pairs);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, content).Result;
                string rsp = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(rsp);
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    var t = await response.Content.ReadAsAsync<TokenModel>();
                   
                    return t;
                }
                return null;
            }
        }
    }
}
