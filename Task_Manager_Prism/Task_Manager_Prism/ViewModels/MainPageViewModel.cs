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
using Task_Manager_Prism.Utils;

namespace Task_Manager_Prism.ViewModels
{

    public class MainPageViewModel : ViewModelBase
    {
        private readonly IDatabaseAccessService _databaseAccessService;
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private int _currentListID;
        private bool _isLoading;
        private ObservableCollection<TaskItem> _allTask;
        private ObservableCollection<TaskItem> _todayTask;
        private ObservableCollection<TaskItem> _searchResultTasks;
        private ObservableCollection<String> _allTags;
        private DelegateCommand<int?> _selectTaskItemDelegateCommand;
        private DelegateCommand<string> _selectTaskItemWithTag;
        private DelegateCommand<string> _addTagToDatabase;
        private DelegateCommand<int> _deleteTaskItemDelegateCommand;
        private DelegateCommand<string> _selectTagItemDelegateCommand;
        private DelegateCommand _reloadCommand;
        private DelegateCommand _logoutCommand;
        private DelegateCommand<int?> _loadListCommand;
        private ObservableCollection<TaskItem> _tasksToShow;


        public MainPageViewModel(IDatabaseAccessService databaseAccessService, INavigationService navigationService, IMessageService messageService)
        {
            _databaseAccessService = databaseAccessService;
            _navigationService = navigationService;
            _messageService = messageService;
            _currentListID = Constants.OverdueTaskListID;
          
        }

        public DelegateCommand<int?> LoadListCommand
        {
            get
            {
                if (_loadListCommand == null)
                {
                    _loadListCommand = new DelegateCommand<int?>( i =>
                    {
                        TasksToShow = LoadNewList(i);
                        if (i == Constants.OverdueTaskListID)
                        {
                            TodayTasks = new ObservableCollection<TaskItem>(AllTasks.Where(s => s.Date.Trim().Equals(DateTime.Today.ToString("MM/dd/yyyy"))));
                        }
                        
                        AllTags = new ObservableCollection<string>(AllTags);
                    }   
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
        public bool IsLoading
        {
            set
            {
                SetProperty(ref _isLoading, value);
            }
            get
            {
                return _isLoading;
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
            get
            {
                return _allTags;
            }
            set
            {
                if (_allTags == null)
                {
                    value.Add("None");
                    value.Add("All");
                }
                if (_allTags != null)
                {
                    if (value.Where(s => s == "None").Count() == 0)
                    {
                        value.Add("None");
                    }
                    if (value.Where(s => s == "All").Count() == 0)
                    {
                        value.Add("All");
                    }
                }
                    
                SetProperty(ref _allTags, value);
            }
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
                        navParameters.Add("list", AllTasks);
                        _navigationService.Navigate("Detail", navParameters);
                     
                    });
                   

                }
                else
                {
                    return _selectTaskItemDelegateCommand;
                }
            }
            
        }
        public DelegateCommand<string> SelectTagItemDelegateCommand
        {
            get
            {
                if (_selectTagItemDelegateCommand == null)
                {
                    return _selectTagItemDelegateCommand = new DelegateCommand<string>((tag) => {
                        
                        CurrentListTagFilter(tag);
                    });
                }
                else
                {

                    return _selectTagItemDelegateCommand;
                }
            }

        }

        private void CurrentListTagFilter(string tag)
        {
        if (tag.Trim() == "All")
            {
                TasksToShow = LoadNewList(_currentListID);
                TodayTasks = new ObservableCollection<TaskItem>(AllTasks.Where(s => s.Date.Trim().Equals(DateTime.Today.ToString("MM/dd/yyyy"))));
                Debug.WriteLine("debug check point");

                return;
            }
         TasksToShow = new ObservableCollection<TaskItem>(LoadNewList(_currentListID).Where(s => s.Tag.Trim() == tag.Trim()).ToList());
         TodayTasks = new ObservableCollection<TaskItem>(AllTasks.Where(s => s.Date.Trim().Equals(DateTime.Today.ToString("MM/dd/yyyy")) && s.Tag.Trim() == tag.Trim()));
         
        }


        public DelegateCommand ReloadCommand
        {
            get
            {
                if (_reloadCommand == null)
                {
                    _reloadCommand = new DelegateCommand(async () =>
                    {
                        IsLoading = true;
                        List<TaskItem> items = await _databaseAccessService.GetTasks();
                        List<TagItem> tags = await _databaseAccessService.GetTags();
                        AllTags = new ObservableCollection<String>((from tag in tags
                                                                 select tag.Name).ToList<String>()
                                                                    ); 
                        AllTasks = new ObservableCollection<TaskItem>(items);
                        TodayTasks = new ObservableCollection<TaskItem>(AllTasks.Where(s => s.Date.Trim().Equals(DateTime.Today.ToString("MM/dd/yyyy"))));
                        TasksToShow = LoadNewList(_currentListID);
                        IsLoading = false;
                       
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
                    return _addTagToDatabase = new DelegateCommand<string>( async s =>
                    {
                        s = s.Trim();
                        if (s=="None"||s=="All")
                        {
                            _messageService.ShowMessage("Error", "Tag 'None' and 'All' can not be used!");
                            return;
                        }
                        await _databaseAccessService.AddTagItem(s);
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
                        if (s == "All")
                        {
                            TasksToShow = AllTasks;
                            return;
                        }
                        TasksToShow = new ObservableCollection<TaskItem>(AllTasks.Where(item => item.Tag.Trim() == s));
                    });
                }

                return _selectTaskItemWithTag;
            }
        }

        

        private ObservableCollection<TaskItem> LoadNewList(int? listID)
        {
            _currentListID = listID??Constants.OverdueTaskListID;
            switch (listID)
            {
                case Constants.AllTaskListID:
                    return AllTasks;
                   
                case Constants.TodayTaskListID:
                    return new ObservableCollection<TaskItem>(AllTasks.Where(s => s.Date.Trim().Equals(DateTime.Today.ToString("MM/dd/yyyy"))));
                   
                case Constants.FinishedTaskListID:
                    return new ObservableCollection<TaskItem>(AllTasks.Where(s => s.IsFinished == true).ToList());
                    
                case Constants.ImportantTaskListID:
                    return new ObservableCollection<TaskItem>(AllTasks.Where(s => s.IsImportant == true));
                    
                case Constants.NormalTaskListID:
                    return new ObservableCollection<TaskItem>(AllTasks.Where(s => s.IsNormal == true));
                    
                case Constants.NoneDateTaskListID:
                    return new ObservableCollection<TaskItem>(AllTasks.Where(s => s.IsTimeConstraint == false));
               
                case Constants.OverdueTaskListID:
                   return new ObservableCollection<TaskItem>(AllTasks.Where(s =>
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

                default:
                    return new ObservableCollection<TaskItem>();



            }
        }
    }
}



