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
    public class TagController : ApiController
    {
        private IDatabaseAccessService _databaseAccessService;
        public TagController(IDatabaseAccessService databaseAccessService)
        {
            _databaseAccessService = databaseAccessService;
        }
        public List<TagItem> GetTagItems()
        {
            var tags = _databaseAccessService.GetTags().Result;
            if (tags == null)
            {
                tags = new List<TagItem>();
            }
            return tags;
        }
        public IHttpActionResult PostTag(TagItem item)
        {
            
            bool isCompleted = _databaseAccessService.AddTagItem(item.Name);
            if (isCompleted)
            {
                return Ok();
            } else
            {
                return BadRequest();
            }
        }
    }
}
