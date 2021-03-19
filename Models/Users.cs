using System;
using System.Collections.Generic;

namespace InventoryService.Models
{
    public partial class Users
    {
       
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ToDo> ToDo { get; set; }
    }
}
