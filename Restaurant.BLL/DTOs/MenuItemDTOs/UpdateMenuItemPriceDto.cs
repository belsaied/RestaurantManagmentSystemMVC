using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.MenuItemDTOs
{
    public class UpdateMenuItemPriceDto
    {
        [Required(ErrorMessage = "Menu item ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid menu item ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 9999.99, ErrorMessage = "Price must be between 0.01 and 9999.99")]
        public decimal Price { get; set; }
    }
}
