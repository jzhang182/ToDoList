using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slb.Bootcamp.Service.ToDoList.Models
{
    public class ToDoItem
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime CreatedTime { get; set; }

        public bool Done { get; set; }

        public bool Favorite { get; set; }

        public ToDoItem[] Children { get; set; }
    }
}
