using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_Prism.Ultils;

namespace Task_Manager_Prism.Utils
{
    class RegisterService : IRegisterService
    {
        private string _error; 
        public string GetError()
        {
            return _error;
        }

        async public Task<bool> RegisterAsync(string username, string password, string confirmPassword)
        {
            Task<bool> registerTask = DoRegister(Constants.RegisterAccountBaseUrl, username, password, confirmPassword);

            return await registerTask;
        }
        private async Task<bool> DoRegister(string url, string userName, string password, string confirmPassword)
        {
            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "Username", userName ),
                        new KeyValuePair<string, string>( "Password", password ),
                        new KeyValuePair<string, string> ( "ConfirmPassword", confirmPassword)
                    };
            var content = new FormUrlEncodedContent(pairs);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(url, content);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {

                        return true;
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        _error = "Error, username is used!";
                        return false;
                    } else
                    {
                        _error = "Error, unknown response code!";
                        return false;
                    }
                    
                }
            } catch (Exception e)
            {
                _error = "Error while trying to make connection: " + e.Message;
                return false;
            }
        }
    }
}
