using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Task_Manager_Prism.Models;
using Task_Manager_Prism.Ultils;
using Task_Manager_Prism.DatabaseAccess;


namespace Task_Manager_Prism.ViewModels
{

    public class EditTaskPageViewModel : ViewModelBase
    {
        private readonly IDatabaseAccessService _databaseAccessService;
        private readonly INavigationService _navigationService;
        private DelegateCommand<TaskItem> _updateTaskItemDelegateCommand;
        private DelegateCommand _backCommand;
        private TaskItem _selectedItem;
        private ObservableCollection<String> _allTags;


        public EditTaskPageViewModel(IDatabaseAccessService databaseAccessService, INavigationService navigationService)
        {
            _databaseAccessService = databaseAccessService;
            _navigationService = navigationService;
            LoadTag();
        }

        public ObservableCollection<string> AllTags
        {
            get { return _allTags; }
            set { SetProperty(ref _allTags, value); }
        }

        public async void LoadTag()
        {
           
            List<TagItem> tags = await _databaseAccessService.GetTags();
            AllTags = new ObservableCollection<String>((from tag in tags
                                                        select tag.Name).ToList<String>());

        }
        public DelegateCommand<TaskItem> UpdateCommand
        {
            get
            {
                if (_updateTaskItemDelegateCommand == null)
                {
                    return _updateTaskItemDelegateCommand = new DelegateCommand<TaskItem>( s =>
                    {
                        s.ID = SelectedItem.ID;
                        _databaseAccessService.UpdateTaskItem(s);
                        var navParameters = new Dictionary<string, object>();
                        navParameters.Add(Constants.SelectedTaskKey, s);
                        _navigationService.Navigate("Detail", navParameters);

                    });
                }

                return _updateTaskItemDelegateCommand;
            }
        }
        public DelegateCommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    return _backCommand = new DelegateCommand(() =>
                    {
                        var navParameters = new Dictionary<string, object>();
                        navParameters.Add(Constants.SelectedTaskKey, SelectedItem);
                        _navigationService.Navigate("Detail", navParameters);
                    });
                }

                return _backCommand;
            }
        }
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {

            if (e != null)
            {
                var dictionary = e.Parameter as Dictionary<string, object>;
                object selectedTask;
                dictionary.TryGetValue(Constants.SelectedTaskKey, out selectedTask);
                SelectedItem = selectedTask as TaskItem;

            }

        }
        public TaskItem SelectedItem
        {
            set
            {
                SetProperty(ref _selectedItem, value);
            }
            get
            {
                return _selectedItem;
            }
        }


    }
}



