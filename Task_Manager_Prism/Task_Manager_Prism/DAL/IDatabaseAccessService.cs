using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_Prism.Models;

namespace Task_Manager_Prism.DatabaseAccess
{
    public interface IDatabaseAccessService
    {

        Task<List<TaskItem>> GetTasks();
        Task<List<TagItem>> GetTags();
        void AddTaskItem(TaskItem item);
        void UpdateTaskItem(TaskItem item);
        void DeleteTaskItem(int id);
        void AddTagItem(string tagName);
    }

}
