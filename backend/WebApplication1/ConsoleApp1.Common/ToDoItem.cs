using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Common
{
    public class ToDoItem
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public bool Done { get; set; }
        public bool Favorite { get; set; }
        public List<ToDoItem> Children { get; set; }
    }
}
