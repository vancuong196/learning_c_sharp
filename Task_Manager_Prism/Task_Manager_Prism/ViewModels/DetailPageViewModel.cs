using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Taskmanager.DatabaseAccess;
using Taskmanager.Models;

namespace Task_Manager_Prism.ViewModels
{
    class DetailPageViewModel : ViewModelBase,INavigationAware
    {
        private IDatabaseAccessService _databaseAccessService;
        public DetailPageViewModel(IDatabaseAccessService databaseAccessService)
        {
            _databaseAccessService = databaseAccessService;
        }

        public TaskItem CurrentTask
        {
            set;
            get;
        }
      
    }
}
