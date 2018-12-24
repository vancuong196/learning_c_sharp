using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskmanager.Models;

namespace Taskmanager.DatabaseAccess
{
    public interface IDatabaseAccessService
    {
        Task<List<TaskItem>> GetTasks();
        Task<List<TagItem>> GetTags();
        void SaveTaskItem(TaskItem item);
    }

}
