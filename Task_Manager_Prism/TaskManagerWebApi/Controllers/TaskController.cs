using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task_Manager_Prism.DatabaseAccess;
using Task_Manager_Prism.Models;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.Security.Claims;
using System.Web;

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
            if (GetIdentity() == null)
            {
              
                return new List<TaskItem>();
            } else
            {
                _databaseAccessService.SetCurrentUser(GetIdentity());
            }
            var tasks=  _databaseAccessService.GetTasks().Result;
            if (tasks == null)
            {
           
                return new List<TaskItem>();
            }
           // Debug.WriteLine("User -----------------------:" + id);
            return tasks;

        }
        

        public IHttpActionResult PostTask(TaskItem item)
        {
            if (GetIdentity() == null)
            {
                return BadRequest();
            }
            else
            {
                _databaseAccessService.SetCurrentUser(GetIdentity());
            }
            bool isCompleted = _databaseAccessService.AddTaskItem(item);
            if (isCompleted)
            {
                return Ok();
            }
            return BadRequest();
            
        }
        public IHttpActionResult PutTask(TaskItem item)
        {
            if (GetIdentity() == null)
            {
                return BadRequest();
            }
            else
            {
                _databaseAccessService.SetCurrentUser(GetIdentity());
            }
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
            if (GetIdentity() == null)
            {
                return BadRequest();
            }
            else
            {
                _databaseAccessService.SetCurrentUser(GetIdentity());
            }
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
        private string GetIdentity()
        {

            var claimsIdentity = User.Identity as ClaimsIdentity;

            foreach (var claim in claimsIdentity.Claims)
            {
                if (claim.Type == "sub")
                {
                    return claim.Value;
                }    
            }
            return null;
        }

    }
}
