using CalculateMathExpression.DAL;
using CalculateMathExpression.Models;
using CalculateMathExpression.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.ViewModels
{
    class PermissionTabViewModel: ViewModelBase
    {
        private IInfomationService _infomationService;
        private IDataAccessService _dataAccessService;
        private Dictionary<string, bool> _permissionsDictionary;
        private RelayCommand _saveCommand;
        private RelayCommand _loadCommand;
       
        public Dictionary<string, bool> Permissons
        {
            set
            {
                Set(ref _permissionsDictionary, value);
            }
            get
            {
                return _permissionsDictionary;
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(()=>
                    {
                        
                    });
                    return SaveCommand;
                }
                else
                {
                    return _saveCommand;
                }
            }
        }
        public RelayCommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(()=>
                    {
                        Load();
                    });
                    return _loadCommand;
                }
                else
                {
                    return _loadCommand;
                }
            }
        }
        public PermissionTabViewModel(IInfomationService informationService, IDataAccessService dataAccessService)
        {
            this._dataAccessService = dataAccessService;
            this._infomationService = informationService;
            _permissionsDictionary = new Dictionary<string, bool>();
            _permissionsDictionary.Add("ONE", false);
            _permissionsDictionary.Add("TWO", true);
            Permissons = _permissionsDictionary;
            LoadCommand.Execute(null);

        }
        public async void Load()
        {
            List<ButtonPermission> permissions = await _dataAccessService.GetButtonPermissionAsync();
            if (permissions==null)
            {
                Debug.WriteLine("Loi doc file");
            }
            foreach (ButtonPermission b in permissions)
            {
                Debug.WriteLine(b.Code);
            }
        }
    }
}
