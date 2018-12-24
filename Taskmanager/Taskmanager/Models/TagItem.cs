using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskmanager.Models
{
    public class TagItem
    {
        public string Name
        {
            get;
            set;
        }
        public TagItem(string tagName)
        {
            Name = tagName;
        }
        public TagItem()
        {

        }
    }
}
