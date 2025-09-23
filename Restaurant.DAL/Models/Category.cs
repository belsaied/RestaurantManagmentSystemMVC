using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Models
{
    public class Category:baseEntity
    {
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties
        public  ICollection<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();
    }
}
