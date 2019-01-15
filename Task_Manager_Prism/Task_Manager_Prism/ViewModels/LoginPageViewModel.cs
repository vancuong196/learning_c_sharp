using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Task_Manager_Prism.Utils;

namespace Task_Manager_Prism.ViewModels
{
    class LoginPageViewModel: ViewModelBase
    {
        private ILoginService _loginService;
        private IMessageService _messageService;
        private INavigationService _navigationService;
        private DelegateCommand _loginCommand;
        private DelegateCommand _toRegisterPageCommand;
        private bool _isProgressVisible;
        private bool _isButtonClickAble;
        public bool IsProgressVisible
        {
           set
            {
                SetProperty(ref _isProgressVisible, value);
            }
            get
            {
                return _isProgressVisible;
            }
        }
        
        public bool IsClickAble
        {
            set
            {
                SetProperty(ref _isButtonClickAble, value);
            }
            get
            {
                return _isButtonClickAble;
            }
        }
        public DelegateCommand ToRegisterPageCommand
        {
            get
            {
                if (_toRegisterPageCommand == null)
                {
                    _toRegisterPageCommand = new DelegateCommand(() =>
                    {
                        _navigationService.Navigate("Register", null);
                    });
                }
                return _toRegisterPageCommand;
            }
        }
        public DelegateCommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new DelegateCommand(async () =>
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
                        IsProgressVisible = true;
                        IsClickAble = false;
                        var loginTask = await _loginService.Login(UserName, Password);
                       
                        IsProgressVisible = false;
                        IsClickAble = true;
                        if (loginTask == true)
                        {
                            ApplicaitonShareDataService.GetCurrent().Token = _loginService.GetToken();
                            _navigationService.Navigate("Main", null);
                           
                        }
                        else
                        {
                            ApplicaitonShareDataService.GetCurrent().Token = null;
                            _messageService.ShowMessage("Error", _loginService.GetError());
                            
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
            IsClickAble = true;
        }
    }
}
