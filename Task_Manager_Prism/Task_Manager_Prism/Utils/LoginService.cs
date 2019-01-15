
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Task_Manager_Prism.Models;
using Task_Manager_Prism.Ultils;
using Prism.Unity.Windows;
using Microsoft.Practices.Unity;

namespace Task_Manager_Prism.Utils
{
    class LoginService : ILoginService
    {
        TokenModel _tokenModel;
        private string _error;
        public string GetError()
        {
            return _error;
        }

        public TokenModel GetToken()
        {
            return _tokenModel;
        }


        async Task<bool> ILoginService.Login(string userName, string password)
        {

            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", userName ),
                        new KeyValuePair<string, string> ( "Password", password )
                    };
            var content = new FormUrlEncodedContent(pairs);
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = new HttpClient())
                {
                    var responseTask = client.PostAsync(Constants.TokenBaseUrl, content);
                    
                    var response = await responseTask;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {

                        var t = response.Content.ReadAsAsync<TokenModel>();
                        t.Wait();
                        _tokenModel = t.Result;
                        if (_tokenModel != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    } else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        _error = "Wrong user name or password!";
                        return false;
                    } else
                    {
                        _error = "Unknown responde from server!";
                        return false;
                    }
                    
                    
                }
            }
            catch (Exception e)
            {
                _error = "Connection error!";
                return false;
            }

            
        }
     
    }
}
