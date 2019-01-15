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
        bool AddTaskItem(TaskItem item);
        bool UpdateTaskItem(TaskItem item);
        bool DeleteTaskItem(int id);
        bool AddTagItem(string tagName);
        bool FindTaskById(int id);
    }

}
