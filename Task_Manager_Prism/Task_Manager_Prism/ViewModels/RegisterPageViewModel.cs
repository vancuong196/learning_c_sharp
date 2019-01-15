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
    class RegisterPageViewModel: ViewModelBase
    {
        private IRegisterService _registerService;
        private IMessageService _messageService;
        private INavigationService _navigationService;
        private DelegateCommand _registerCommand;
        private DelegateCommand _toLoginPageCommand;
        private bool _isProgressActive;
        private bool _isButtonClickAble;

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
        public bool IsProgressActive
        {
            set
            {
                SetProperty(ref _isProgressActive, value);
            }
            get
            {
                return _isProgressActive;
            }
        }
        public DelegateCommand ToLoginPageCommand
        {
            get
            {
                if (_toLoginPageCommand == null)
                {
                    _toLoginPageCommand = new DelegateCommand(() =>
                    {
                        _navigationService.Navigate("Login", null);
                    });
                }
                return _toLoginPageCommand;
            }
        }
        public DelegateCommand RegisterCommand
        {
            get
            {
                if (_registerCommand == null)
                {
                    _registerCommand = new DelegateCommand(async() =>
                    {
                        
                        if (UserName == "" || UserName == null)
                        {
                            _messageService.ShowMessage("Error", "UserName can not be empty!");
                            return;
                        }
                        if (Password == null || Password =="" || Password.Length<6)
                        {
                            _messageService.ShowMessage("Error", "Password can not be empty or less than 6 characters!");
                            return;
                        }
                        if (ConfirmPassword == null || ConfirmPassword == "" || ConfirmPassword.Length < 6)
                        {
                            _messageService.ShowMessage("Error", "ConfirmPassword can not be empty or less than 6 characters!");
                            return;
                        }
                        if (!ConfirmPassword.Equals(Password))
                        {
                            _messageService.ShowMessage("Error", "Wrong confirm password!");
                            return;
                        }
                        IsProgressActive = true;
                        var isCompleted = await _registerService.RegisterAsync(UserName, Password, ConfirmPassword);
                        IsProgressActive = false;
                        if (isCompleted ==true)
                        {
                            _messageService.ShowMessage("Register completed!");
                            _navigationService.Navigate("Login", null);

                        }
                        else
                        {
                           
                            _messageService.ShowMessage("Error", _registerService.GetError());

                        }
                    });
                }
                return _registerCommand;
            }
        }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string ConfirmPassword { set; get; }
        public RegisterPageViewModel(IRegisterService registerService, IMessageService messageService, INavigationService navigationService)
        {
            _registerService = registerService;
            _messageService = messageService;
            _navigationService = navigationService;
            _isButtonClickAble = true;
        }
    }
}
