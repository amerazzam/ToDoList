using System;
using System.Collections.Generic;

namespace ToDoList.Models
{
    public partial class ToDo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public DateTime StartDate { get; set; }

        public virtual Users User { get; set; }

    }
}
