using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
            if (GetIdentity() == null)
            {
                return new List<TagItem>();
            }
            else
            {
                _databaseAccessService.SetCurrentUser(GetIdentity());
            }
            var tags = _databaseAccessService.GetTags().Result;
            if (tags == null)
            {
                tags = new List<TagItem>();
            }
            return tags;
        }
        public IHttpActionResult PostTag(TagItem item)
        {
            if (GetIdentity() == null)
            {
                return BadRequest();
            }
            else
            {
                _databaseAccessService.SetCurrentUser(GetIdentity());
            }

            if (_databaseAccessService.FindTagByName(item.Name))
            {
                return BadRequest();
            }

            bool isCompleted = _databaseAccessService.AddTagItem(item.Name);
            if (isCompleted)
            {
                return Ok();
            } else
            {
                return BadRequest();
            }
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
