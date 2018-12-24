using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskmanager.Models;

namespace Taskmanager.DatabaseAccess
{
    public class FakeDatabaseAccessService : IDatabaseAccessService
    {
        public Task<List<TaskItem>> GetTasks()
        {
            List<TaskItem> taskList = new List<TaskItem>();
            taskList.Add(new TaskItem(1,"Test1", "12:30", "10/10/2018", "Descrition1", false, "Todo"));


            return Task.FromResult(taskList);
            
        }
        public Task<List<TagItem>> GetTags()
        {
            var tagList = new List<TagItem>();
            tagList.Add(new TagItem("Todo"));
            tagList.Add(new TagItem("Must do"));
            tagList.Add(new TagItem("Remind"));
            return Task.FromResult(tagList);
        }

        public void SaveTaskItem(TaskItem item)
        {
            throw new NotImplementedException();
        }
    }
}
