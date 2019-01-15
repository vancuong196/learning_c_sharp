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
            var tasks=  _databaseAccessService.GetTasks().Result;
            if (tasks == null)
            {
                return new List<TaskItem>();
            }
            return tasks;
        }

        public IHttpActionResult PostTask(TaskItem item)
        {
            bool isCompleted = _databaseAccessService.AddTaskItem(item);
            if (isCompleted)
            {
                return Ok();
            }
            return BadRequest();
            
        }
        public IHttpActionResult PutTask(TaskItem item)
        {
            bool isAvailable = _databaseAccessService.FindTaskById(item.ID);
            if (!isAvailable)
            {
                return NotFound();
            }
            bool isCompleted = _databaseAccessService.UpdateTaskItem(item);
            if (isCompleted)
            {
                return Ok();
            }
            return BadRequest();
        }
        public IHttpActionResult DeleteTask(int id)
        {
            bool isAvailable = _databaseAccessService.FindTaskById(id);
            if (!isAvailable)
            {
                return NotFound();
            }

            bool isCompleted = _databaseAccessService.DeleteTaskItem(id);
            if (isCompleted)
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}
