using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskmanager.DatabaseAccess;
using Taskmanager.Models;

namespace Task_Manager_Prism.ViewModels
{
    class AddTaskPageViewModel : ViewModelBase
    {
        private ObservableCollection<String> _allTags;
        private IDatabaseAccessService _databaseAccessService;
        private DelegateCommand<TaskItem> _saveTaskItemDelegateCommand;

        public AddTaskPageViewModel(IDatabaseAccessService databaseAccessService)
        {
            _databaseAccessService = databaseAccessService;
            LoadTag();
        }
        private async void LoadTag() 
                    {
                       
        List<TagItem> tags = await _databaseAccessService.GetTags();
        AllTags = new ObservableCollection<String>((from tag in tags
                                                    select tag.Name).ToList<String>()
                                                                    );
                       
                        }
        public DelegateCommand<TaskItem> SaveCommand
        {
            get
            {
                if (_saveTaskItemDelegateCommand == null)
                {
                    return _saveTaskItemDelegateCommand = new DelegateCommand<TaskItem>(s =>
                    {
                        Debug.WriteLine(s.ToString());
                        _databaseAccessService.AddTaskItem(s);

                    });
                }

                return _saveTaskItemDelegateCommand;
            }
        }

        public ObservableCollection<string> AllTags
        {
            get { return _allTags; }
            set { SetProperty(ref _allTags, value); }
        }


    }
}
