using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.MenuItemDTOs
{
    public class MenuItemsByCategoryDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; } = string.Empty;
         public decimal price { get; set; }
          public string ImageName { get; set; }
          public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }    
    }
}
