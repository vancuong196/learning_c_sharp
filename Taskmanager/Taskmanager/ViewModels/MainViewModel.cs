using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Taskmanager.DatabaseAccess;
using Taskmanager.Models;


namespace Taskmanager.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private readonly IDatabaseAccessService _databaseAccessService;
        private ObservableCollection<TaskItem> _allTask;
        private ObservableCollection<TaskItem> _importantTasks;
        private ObservableCollection<TaskItem> _finishedTasks;
        private ObservableCollection<TaskItem> _overdueTasks;
        private ObservableCollection<TaskItem> _todayTasks;
        private ObservableCollection<TaskItem> _normalTasks;
        private ObservableCollection<TaskItem> _noDateTasks;
        private ObservableCollection<TagItem> _allTags;
        private TaskItem _selectedItem;
        private RelayCommand<int> _selecteTaskItemRelayCommand;
        private RelayCommand<TaskItem> _saveTaskItemRelayCommand;
        private RelayCommand _nextTaskItemRelayCommand;
        private RelayCommand _previousTaskItemRelayCommand;
        private RelayCommand _deleteTaskItemRelayCommand;
        private RelayCommand _reloadCommand;


        public MainViewModel(IDatabaseAccessService databaseAccessService)
        {
            _databaseAccessService = databaseAccessService;

            LoadCommand.Execute(null);
        }

        private void SelectItemFromTaskList(int id)
        {
            TaskItem item = _allTask.FirstOrDefault(s => s.ID == id);
            Debug.WriteLine("selected: " + item.Title);
            SelectedItem = item;
        }

        public TaskItem SelectedItem
        {
            set
            {
                Set(ref _selectedItem, value);
            }
            get
            {
                return _selectedItem;
            }
        }
        

        public ObservableCollection<TaskItem> AllTasks

        {
            get { return _allTask; }
            set { Set(ref _allTask, value); }
        }

        public ObservableCollection<TaskItem> OverdueTasks

        {
            get { return _overdueTasks; }
            set { Set(ref _overdueTasks, value); }
        }

        public ObservableCollection<TagItem> AllTags
        {
            get { return _allTags; }
            set { Set(ref _allTags, value); }
        }

        public ObservableCollection<TaskItem> ImportantTasks
        {
            set
            {
                Set(ref _importantTasks, value);
            }
            get
            {
                return _importantTasks;
            }
        }
        public ObservableCollection<TaskItem> FinishedTasks
        {
            set
            {
                Set(ref _finishedTasks, value);
            }
            get
            {
                return _finishedTasks;
            }
        }
        public ObservableCollection<TaskItem> NormalTasks
        {
            set
            {
                Set(ref _normalTasks, value);
            }
            get
            {
                return _normalTasks;
            }
        }
        public ObservableCollection<TaskItem> NoDateTasks
        {
            set
            {
                Set(ref _noDateTasks, value);
            }
            get
            {
                return _noDateTasks;
            }
        }
        public ObservableCollection<TaskItem> TodayTasks
        {
            set
            {
                Set(ref _todayTasks, value);
            }
            get
            {
                return _todayTasks;
            }
        }

        public RelayCommand<int> SelectListItemRelayCommand
        {
            get
            {
               if (_selecteTaskItemRelayCommand== null)
                {
                    return _selecteTaskItemRelayCommand = new RelayCommand<int>((itemID) => SelectItemFromTaskList(itemID));
                }
                else
                {
                    return _selecteTaskItemRelayCommand;
                }
            }
            
        }


        

        public RelayCommand LoadCommand
        {
            get
            {
                if (_reloadCommand == null)
                {
                    _reloadCommand = new RelayCommand(async () =>
                    {
                        List<TaskItem> items = await _databaseAccessService.GetTasks();
                        List<TagItem> tags = await _databaseAccessService.GetTags();
                        AllTags = new ObservableCollection<TagItem>(tags);
                        NoDateTasks =new ObservableCollection<TaskItem>( items.Where(s => s.Date.Trim() == ""||s.Date==null));
                        ImportantTasks =new ObservableCollection<TaskItem>( items.Where(s => s.IsImportant));
                        NormalTasks = new ObservableCollection<TaskItem>(items.Where(s => !s.IsImportant));
                        TodayTasks =new ObservableCollection<TaskItem>( items.Where(s => s.Date.Trim().Equals(DateTime.Today.ToString("dd/MM/yyyy"))));
                        //Todo fix here!
                        OverdueTasks =new ObservableCollection<TaskItem>( items.Where(s => {
                            //   DateTime d1;
                            if (!s.IsTimeConstraint|| s.IsFinished)
                            {
                                return false;
                            }

                            DateTime taskDay = DateTime.ParseExact(s.Date.Trim(), "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                            Debug.WriteLine(taskDay.ToString());
                            DateTime currentDay = DateTime.ParseExact(DateTime.Today.ToString("dd/MM/yyyy"), "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                            Debug.WriteLine(currentDay.ToString());
                            if (currentDay>taskDay)
                            {
                                return true;
                            }
                            if (taskDay == currentDay)
                            {
                                if (s.Time.Trim().CompareTo(DateTime.Now.ToString("HH:mm:ss"))==-1)
                                {
                                    return true;
                                }
                            }
                            return false;
                            }
                        ));
                       
                        AllTasks = new ObservableCollection<TaskItem>(items);
                        FinishedTasks = new ObservableCollection<TaskItem>(items.Where(s=>s.IsFinished));
                    });
                }

                return _reloadCommand;
            }
        }
        public RelayCommand<TaskItem> SaveCommand
        {
            get
            {
                if (_saveTaskItemRelayCommand == null)
                {
                    return _saveTaskItemRelayCommand = new RelayCommand<TaskItem>(s =>
                    {
                        Debug.WriteLine(s.Date+" "+s.Description+" "+s.Title+" "+s.Time+" "+s.Tag );
                    });
                }

                return _saveTaskItemRelayCommand;
            }
        }
        public RelayCommand NextItemCommand
        {
            get
            {
                if (_nextTaskItemRelayCommand == null)
                {
                    _nextTaskItemRelayCommand = new RelayCommand(() =>
                    {
                        TaskItem item = _allTask[0];
                        if (item == null)
                        {
                            SelectedItem = _allTask.Last();
                        }
                        else
                        {
                            SelectedItem = item;
                        }
                    });
                    return _nextTaskItemRelayCommand;
                }
                else
                {
                    return _nextTaskItemRelayCommand;
                }
            }
        }
        public RelayCommand PreviousItemCommand
        {
            get
            {
                if (_nextTaskItemRelayCommand == null)
                {
                    _nextTaskItemRelayCommand = new RelayCommand(() =>
                    {
                        TaskItem item = _allTask[0];
                        if (item == null)
                        {
                            SelectedItem = _allTask.Last();
                        }
                        else
                        {
                            SelectedItem = item;
                        }
                    });
                    return _nextTaskItemRelayCommand;
                }
                else
                {
                    return _nextTaskItemRelayCommand;
                }
            }
        }


    }
}



