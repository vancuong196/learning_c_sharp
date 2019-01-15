using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Task_Manager_Prism.Models;
using Task_Manager_Prism.Ultils;
using Task_Manager_Prism.DatabaseAccess;



namespace Task_Manager_Prism.ViewModels
{

    public class MainPageViewModel : ViewModelBase
    {
        private readonly IDatabaseAccessService _databaseAccessService;
        private readonly INavigationService _navigationService;
        private int _currentListID;
        private ObservableCollection<TaskItem> _allTask;
        private ObservableCollection<TaskItem> _todayTask;
        private ObservableCollection<TaskItem> _searchResultTasks;
        private ObservableCollection<String> _allTags;
        private DelegateCommand<int?> _selectTaskItemDelegateCommand;
        private DelegateCommand<string> _selectTaskItemWithTag;
        private DelegateCommand<string> _addTagToDatabase;
        private DelegateCommand<int> _deleteTaskItemDelegateCommand;
        private DelegateCommand _reloadCommand;
        private DelegateCommand _logoutCommand;
        private DelegateCommand<int?> _loadListCommand;
        private ObservableCollection<TaskItem> _tasksToShow;


        public MainPageViewModel(IDatabaseAccessService databaseAccessService, INavigationService navigationService)
        {
            _databaseAccessService = databaseAccessService;
            _navigationService = navigationService;
            _currentListID = Constants.OverdueTaskListID;
            Debug.WriteLine("----------------------------------------- ");
           // ReloadCommand.Execute();
        }


        
        public DelegateCommand<int?> LoadListCommand
        {
            get
            {
                if (_loadListCommand == null)
                {
                    _loadListCommand = new DelegateCommand<int?>(LoadNewList
                    );
                    return _loadListCommand;
                }
                return _loadListCommand;
            }
        }

        public DelegateCommand LogoutCommand
        {
            get
            {
                if (_logoutCommand == null)
                {
                    _logoutCommand = new DelegateCommand(() =>
                    {

                        _navigationService.Navigate("Login", null);
                    }
                    );
                    
                }
                return _logoutCommand;
            }
        }

        public ObservableCollection<TaskItem> SearchResultTasks
        {
            set
            {
                SetProperty(ref _searchResultTasks, value);
            }
            get
            {
                return _searchResultTasks;
            }
        }

        public ObservableCollection<TaskItem> TasksToShow
        {
            set
            {
                SetProperty(ref _tasksToShow, value);
            }
            get
            {
                return _tasksToShow;
            }
        }
        public ObservableCollection<TaskItem> TodayTasks
        {
            set
            {
                SetProperty(ref _todayTask, value);
            }
            get
            {
                return _todayTask;
            }
        }

        public ObservableCollection<TaskItem> AllTasks

        {
            get { return _allTask; }
            set { SetProperty(ref _allTask, value); }
        }


        public ObservableCollection<string> AllTags
        {
            get { return _allTags; }
            set { SetProperty(ref _allTags, value); }
        }

   

        public DelegateCommand<int?> SelectListItemDelegateCommand
        {
            get
            {
               if (_selectTaskItemDelegateCommand== null)
                {
                    return _selectTaskItemDelegateCommand = new DelegateCommand<int?>((itemID) => {
                        TaskItem selectedItem = AllTasks.FirstOrDefault(s => s.ID == itemID);
                        var navParameters = new Dictionary<string,object>();
                        navParameters.Add(Constants.SelectedTaskKey, selectedItem);   
                        _navigationService.Navigate("Detail", navParameters);
                     
                    });
                   

                }
                else
                {
                    return _selectTaskItemDelegateCommand;
                }
            }
            
        }


        

        public DelegateCommand ReloadCommand
        {
            get
            {
                if (_reloadCommand == null)
                {
                    _reloadCommand = new DelegateCommand(async () =>
                    {
                        List<TaskItem> items = await _databaseAccessService.GetTasks();
                        List<TagItem> tags = await _databaseAccessService.GetTags();
                        AllTags = new ObservableCollection<String>((from tag in tags
                                                                 select tag.Name).ToList<String>()
                                                                    ); 
                        AllTasks = new ObservableCollection<TaskItem>(items);
                        TodayTasks = new ObservableCollection<TaskItem>(AllTasks.Where(s => s.Date.Trim().Equals(DateTime.Today.ToString("MM/dd/yyyy"))));
                        LoadNewList(_currentListID);
                    });
                }

                return _reloadCommand;
            }
        }
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
               ReloadCommand.Execute();

        }
        public DelegateCommand<string> AddTagToDatabaseCommand
        {
            get
            {
                if (_addTagToDatabase == null)
                {
                    return _addTagToDatabase = new DelegateCommand<string>(s =>
                    {
                        Debug.WriteLine(s.ToString());
                        _databaseAccessService.AddTagItem(s);

                        ReloadCommand.Execute();

                    });
                }

                return _addTagToDatabase;
            }
        }




        public DelegateCommand<string> SelectTaskWithTag
        {
            get
            {
                if (_selectTaskItemWithTag == null)
                {
                    return _selectTaskItemWithTag = new DelegateCommand<string>(s =>
                    {
                        TasksToShow = new ObservableCollection<TaskItem>(AllTasks.Where(item => item.Tag.Trim() == s));


                    });
                }

                return _selectTaskItemWithTag;
            }
        }



    private void LoadNewList(int? listID)
        {
            _currentListID = listID??Constants.OverdueTaskListID;
            switch (listID)
            {
                case Constants.AllTaskListID:
                    TasksToShow = AllTasks;
                    break;
                case Constants.TodayTaskListID:
                    TasksToShow = new ObservableCollection<TaskItem>(AllTasks.Where(s => s.Date.Trim().Equals(DateTime.Today.ToString("MM/dd/yyyy"))));
                    break;
                case Constants.FinishedTaskListID:
                    TasksToShow = new ObservableCollection<TaskItem>(AllTasks.Where(s => s.IsFinished == true).ToList());
                    break;
                case Constants.ImportantTaskListID:
                    TasksToShow = new ObservableCollection<TaskItem>(AllTasks.Where(s => s.IsImportant == true));
                    break;
                case Constants.NormalTaskListID:
                    TasksToShow = new ObservableCollection<TaskItem>(AllTasks.Where(s => s.IsNormal == true));
                    break;
                case Constants.NoneDateTaskListID:
                    TasksToShow = new ObservableCollection<TaskItem>(AllTasks.Where(s => s.IsTimeConstraint == false));
                    break;
                case Constants.OverdueTaskListID:
                    TasksToShow = new ObservableCollection<TaskItem>(AllTasks.Where(s =>
                    {
                        //   DateTime d1;
                        if (!s.IsTimeConstraint || s.IsFinished)
                        {
                            return false;
                        }

                        DateTime taskDay = DateTime.ParseExact(s.Date.Trim(), "MM/dd/yyyy",
                        CultureInfo.InvariantCulture);
                        Debug.WriteLine(taskDay.ToString());
                        DateTime currentDay = DateTime.ParseExact(DateTime.Today.ToString("MM/dd/yyyy"), "MM/dd/yyyy",
                        CultureInfo.InvariantCulture);
                        Debug.WriteLine(currentDay.ToString());
                        if (currentDay > taskDay)
                        {
                            return true;
                        }
                        if (taskDay == currentDay)
                        {
                            if (s.Time.Trim().CompareTo(DateTime.Now.ToString("HH:mm:ss")) == -1)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                   ));
                    break;



            }
        }
    }
}



