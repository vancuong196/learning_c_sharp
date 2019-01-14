using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task_Manager_Prism.DatabaseAccess;
using Task_Manager_Prism.Models;

namespace TaskManagerWebApi.Controllers
{
    [Authorize]
    public class TaskController : ApiController
    {
        IDatabaseAccessService _databaseAccessService;
        public TaskController(IDatabaseAccessService databaseAccessService)
        {
            _databaseAccessService = databaseAccessService;
        }

        public List<TaskItem> GetTaskItems()
        {
            return _databaseAccessService.GetTasks().Result;
        }
        public TaskItem GetTaskItems(int id)
        {
            return _databaseAccessService.GetTasks().Result.FirstOrDefault(s=> s.ID == id);
        }
        public void PostTask(TaskItem item)
        {
            _databaseAccessService.AddTaskItem(item);
            
        }
        public IHttpActionResult PutTask(TaskItem item)
        {

            _databaseAccessService.UpdateTaskItem(item);
            return Ok();
        }
        public IHttpActionResult DeleteTask(int id)
        {
            _databaseAccessService.DeleteTaskItem(id);
            return Ok();
        }

    }
}
