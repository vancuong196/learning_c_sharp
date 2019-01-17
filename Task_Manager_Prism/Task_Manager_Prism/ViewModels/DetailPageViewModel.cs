
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Task_Manager_Prism.Models;
using Task_Manager_Prism.Ultils;
using Task_Manager_Prism.DatabaseAccess;

namespace Task_Manager_Prism.ViewModels
{
    class DetailPageViewModel : ViewModelBase
    {
      
        private TaskItem _selectedItem;
        private IDatabaseAccessService _databaseAccessService;
        private INavigationService _navigationService;
        private ObservableCollection<TaskItem> _allTask;
        private DelegateCommand _nextTaskItemDelegateCommand;
        private DelegateCommand _previousTaskItemDelegateCommand;
        private DelegateCommand _markAsFinishedDelegateComand;
        private DelegateCommand _editTaskDelegateCommand;
        private DelegateCommand<int?> _deleteTaskItemDelegateCommand;
        public DetailPageViewModel(IDatabaseAccessService databaseAccessService,INavigationService navigationService)
        {
            _databaseAccessService = databaseAccessService;
            _navigationService = navigationService;
            
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


        public ObservableCollection<TaskItem> AllTasks

        {
            get { return _allTask; }
            set { SetProperty(ref _allTask, value); }
        }

        private void SelectItemFromTaskList(int? id)
        {
            TaskItem item = _allTask.FirstOrDefault(s => s.ID == id);
            Debug.WriteLine("selected: " + item.Title);
            SelectedItem = item;
        }
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
           // Load();
            if (e != null)
            {
                var dictionary = e.Parameter as Dictionary<string,object>;
                object selectedTask;
                object tasks;
                dictionary.TryGetValue(Constants.SelectedTaskKey, out selectedTask);
                dictionary.TryGetValue("list", out tasks);
                AllTasks = tasks as ObservableCollection<TaskItem>;
                SelectedItem = selectedTask as TaskItem;

            }

        }
        public async void Load()
        {
                        List<TaskItem> items = await _databaseAccessService.GetTasks();
                        AllTasks = new ObservableCollection<TaskItem>(items);

        }

        public DelegateCommand EditCommand
        {
            get
            {
                if (_editTaskDelegateCommand == null)
                {
                    return _editTaskDelegateCommand = new DelegateCommand(() => {
                        
                        var navParameters = new Dictionary<string, object>();
                        navParameters.Add(Constants.SelectedTaskKey, SelectedItem);
                        _navigationService.Navigate("EditTask", navParameters);

                    });
                }
                else
                {
                    return _editTaskDelegateCommand;
                }
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

                        for (int i = 0; i < AllTasks.Count; i++)
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

                        for (int i = AllTasks.Count - 1; i >= 0; i--)
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
                if (_markAsFinishedDelegateComand == null)
                {
                    _markAsFinishedDelegateComand = new DelegateCommand(() =>
                    {
                        SelectedItem.IsFinished = true;
                        TaskItem item = new TaskItem();
                        item.Date = SelectedItem.Date;
                        item.Time = SelectedItem.Time;
                        item.Tag = SelectedItem.Tag;
                        item.IsFinished = SelectedItem.IsFinished;
                        item.ID = SelectedItem.ID;
                        item.IsImportant = SelectedItem.IsImportant;
                        item.Title = SelectedItem.Title;
                        item.Description = SelectedItem.Description;
                        SelectedItem = item;
                        _databaseAccessService.UpdateTaskItem(SelectedItem);
                    });
                    return _markAsFinishedDelegateComand;
                }
                else
                {
                    return _markAsFinishedDelegateComand;
                }
            }
        }
        public DelegateCommand<int?> DeleteCommand
        {
            get
            {
                return _deleteTaskItemDelegateCommand == null
                    ? (_deleteTaskItemDelegateCommand = new DelegateCommand<int?>(s =>
                    {
                        Debug.WriteLine(s.ToString());
                        _databaseAccessService.DeleteTaskItem(s??-1);
                        _navigationService.Navigate("Main", null);

                    }))
                    : _deleteTaskItemDelegateCommand;
            }
        }

    }


}
