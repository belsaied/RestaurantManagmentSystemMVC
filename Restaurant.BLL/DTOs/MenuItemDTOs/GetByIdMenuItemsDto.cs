using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.MenuItemDTOs
{
    public class GetByIdMenuItemsDto 
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImageUrl { get; set; }

    }
}
