using CalculateMathExpression.DAL;
using CalculateMathExpression.Models;
using CalculateMathExpression.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace CalculateMathExpression.ViewModels
{
    class PermissionTabViewModel: ViewModelBase, INotifyPropertyChanged
    {
        private IInfomationService _infomationService;
        private IDataAccessService _dataAccessService;
        public event PropertyChangedEventHandler PropertyChanged;
        private Dictionary<string, bool> _permissionsDictionary;
        private RelayCommand _saveCommand;
        private RelayCommand _loadCommand;
       
        public Dictionary<string, bool> Permissons
        {
            set
            {
                Set(ref _permissionsDictionary, value);
                NotifyPropertyChanged("Permissons");
            }
            get
            {
                return _permissionsDictionary;
            }
        }
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
                        _dataAccessService.Save(_permissionsDictionary);
                        _infomationService.ShowMessgae("Saved!");
                        Permissons = _permissionsDictionary;
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
           
            LoadCommand.Execute(null);

        }
        public async void Load()
        {
            _permissionsDictionary = await _dataAccessService.GetButtonPermissionAsync();
            if (_permissionsDictionary == null || _permissionsDictionary.Keys.Count==0)
            {
                Debug.WriteLine("File error");
                _permissionsDictionary = new Dictionary<string, bool>();
                _permissionsDictionary.Add("ONE", true);
                _permissionsDictionary.Add("TWO", true);
                _permissionsDictionary.Add("THREE", true);
                _permissionsDictionary.Add("FOUR", true);
                _permissionsDictionary.Add("FIVE", true);
                _permissionsDictionary.Add("SIX", true);
                _permissionsDictionary.Add("SEVEN", true);
                _permissionsDictionary.Add("EIGHT", true);
                _permissionsDictionary.Add("NINE", true);
                _permissionsDictionary.Add("SQR", true);
                _permissionsDictionary.Add("SQRT", true);
                _permissionsDictionary.Add("DOT", true);
                _permissionsDictionary.Add("OPENB", true);
                _permissionsDictionary.Add("CLOSEB", true);
                _permissionsDictionary.Add("ADD", true);
                _permissionsDictionary.Add("MUL", true);
                _permissionsDictionary.Add("DIV", true);
                _permissionsDictionary.Add("SUB", true);
                _permissionsDictionary.Add("BACK", true);
                _permissionsDictionary.Add("CLEAR", true);
                _permissionsDictionary.Add("XC1", true);
                _permissionsDictionary.Add("XC2", true);
                _permissionsDictionary.Add("XC3", true);
                _permissionsDictionary.Add("XC4", true);
                _permissionsDictionary.Add("YC1", true);
                _permissionsDictionary.Add("YC2", true);
                _permissionsDictionary.Add("YC3", true);
                _permissionsDictionary.Add("YC4", true);
                _permissionsDictionary.Add("XBC1", true);
                _permissionsDictionary.Add("XBC2", true);
                _permissionsDictionary.Add("XBC3", true);
                _permissionsDictionary.Add("XBC4", true);
                _permissionsDictionary.Add("YBC1", true);
                _permissionsDictionary.Add("YBC2", true);
                _permissionsDictionary.Add("YBC3", true);
                _permissionsDictionary.Add("YBC4", true);



            }
            Permissons = _permissionsDictionary;

        }

    }
}
