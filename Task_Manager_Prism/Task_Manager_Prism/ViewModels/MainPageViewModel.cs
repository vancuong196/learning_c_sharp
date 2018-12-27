using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Task_Manager_Prism.Ultils;
using Taskmanager.DatabaseAccess;
using Taskmanager.Models;


namespace Task_Manager_Prism.ViewModels
{

    public class MainPageViewModel : ViewModelBase
    {
        private readonly IDatabaseAccessService _databaseAccessService;
        private readonly INavigationService _navigationService;
        private int _currentListID;
        private ObservableCollection<TaskItem> _allTask;
        private ObservableCollection<TaskItem> _searchResultTasks;
        private ObservableCollection<String> _allTags;
        private TaskItem _selectedItem;
        private DelegateCommand<int?> _selectTaskItemDelegateCommand;
        private DelegateCommand<TaskItem> _saveTaskItemDelegateCommand;
        private DelegateCommand<TaskItem> _updateTaskItemDelegateCommand;
        private DelegateCommand<string> _selectTaskItemWithTag;
        private DelegateCommand<string> _addTagToDatabase;
        private DelegateCommand _nextTaskItemDelegateCommand;
        private DelegateCommand _previousTaskItemDelegateCommand;
        private DelegateCommand _markAsFinishedRelayComand;
        private DelegateCommand<int> _deleteTaskItemDelegateCommand;
        private DelegateCommand _reloadCommand;
        private DelegateCommand<int?> _loadListCommand;
        private ObservableCollection<TaskItem> _tasksToShow;


        public MainPageViewModel(IDatabaseAccessService databaseAccessService, INavigationService navigationService)
        {
            _databaseAccessService = databaseAccessService;
            _navigationService = navigationService;
            _currentListID = Constants.OverdueTaskListID;
            Debug.WriteLine("----------------------------------------- ");
            ReloadCommand.Execute();
        }

        private void SelectItemFromTaskList(int? id)
        {
            TaskItem item = _allTask.FirstOrDefault(s => s.ID == id);
            Debug.WriteLine("selected: " + item.Title);
            SelectedItem = item;
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
                        SelectItemFromTaskList(itemID);
                        var navParameters = new Dictionary<string,string>();
                        
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
                        LoadNewList(_currentListID);
                    });
                }

                return _reloadCommand;
            }
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
        public DelegateCommand<TaskItem> UpdateCommand
        {
            get
            {
                if (_updateTaskItemDelegateCommand == null)
                {
                    return _updateTaskItemDelegateCommand = new DelegateCommand<TaskItem>(s =>
                    {
                        Debug.WriteLine(s.ToString());
                        s.ID = SelectedItem.ID;
                        _databaseAccessService.UpdateTaskItem(s);
                        SelectedItem = s;
                        ReloadCommand.Execute();

                    });
                }

                return _updateTaskItemDelegateCommand;
            }
        }

        public DelegateCommand<int> DeleteCommand
        {
            get
            {
                if (_deleteTaskItemDelegateCommand == null)
                {
                    return _deleteTaskItemDelegateCommand = new DelegateCommand<int>(s =>
                    {
                        Debug.WriteLine(s.ToString());
                        _databaseAccessService.DeleteTaskItem(s);
                        ReloadCommand.Execute();

                    });
                }

                return _deleteTaskItemDelegateCommand;
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
        public DelegateCommand NextItemCommand
        {
            get
            {
                if (_nextTaskItemDelegateCommand == null)
                {
                    _nextTaskItemDelegateCommand = new DelegateCommand(() =>
                    {
                       
                        for (int i=0; i < AllTasks.Count; i++)
                        {
                            if (AllTasks[i].ID == SelectedItem.ID)
                            {
                                if ((i + 1) < AllTasks.Count)
                                {
                                    SelectedItem = AllTasks[i + 1];
                                    break;
                                }
                            }
                        }
                    });
                    return _nextTaskItemDelegateCommand;
                }
                else
                {
                    return _nextTaskItemDelegateCommand;
                }
            }
        }
        public DelegateCommand PreviousItemCommand
        {
            get
            {
                if (_previousTaskItemDelegateCommand == null)
                {
                    _previousTaskItemDelegateCommand = new DelegateCommand(() =>
                    {

                        for (int i = AllTasks.Count-1; i>=0; i--)
                        {
                            if (AllTasks[i].ID == SelectedItem.ID)
                            {
                                if ((i - 1) >= 0)
                                {
                                    SelectedItem = AllTasks[i - 1];
                                    break;
                                }
                              
                            }
                        }
                    });
                    return _previousTaskItemDelegateCommand;
                }
                else
                {
                    return _nextTaskItemDelegateCommand;
                }
            }
        }
       public DelegateCommand MarkAsFinishedCommand
        {
            get
            {
                if (_markAsFinishedRelayComand == null)
                {
                    _markAsFinishedRelayComand = new DelegateCommand(() =>
                    {

                        SelectedItem.IsFinished = true;
                     
                        UpdateCommand.Execute(SelectedItem);
                    });
                    return _markAsFinishedRelayComand;
                }
                else
                {
                    return _markAsFinishedRelayComand;
                }
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



