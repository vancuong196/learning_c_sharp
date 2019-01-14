using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_Prism.Utils;

namespace Task_Manager_Prism.ViewModels
{
    class LoginPageViewModel: ViewModelBase
    {
        private ILoginService _loginService;
        private IMessageService _messageService;
        private INavigationService _navigationService;
        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new DelegateCommand(() =>
                    {
                        if (UserName==""|| UserName == null)
                        {
                            _messageService.ShowMessage("Error", "UserName can not be empty!");
                            return;
                        }
                        if (Password == null)
                        {
                            _messageService.ShowMessage("Error", "Password can not be empty!");
                            return;
                        }
                        var loginTask = _loginService.Login(UserName, Password);
                        loginTask.Wait();
                        if (loginTask.Result == true)
                        {
                            ApplicaitonShareDataService.GetCurrent().Token = _loginService.GetToken();
                            _navigationService.Navigate("Main", null);
                           
                        }
                        else
                        {
                            ApplicaitonShareDataService.GetCurrent().Token = null;
                            _messageService.ShowMessage("Error", "Can not login now");
                            
                        }
                    });
                }
                return _loginCommand;
            }
        }
        public string UserName { set; get; }
        public string Password { set; get; }
        public  LoginPageViewModel(ILoginService loginService, IMessageService messageService, INavigationService navigationService)
        {
            _loginService = loginService;
            _messageService = messageService;
            _navigationService = navigationService;
        }
    }
}
