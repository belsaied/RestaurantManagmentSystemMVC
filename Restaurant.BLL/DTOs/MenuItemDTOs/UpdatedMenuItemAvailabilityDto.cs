using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.MenuItemDTOs
{
    public class UpdatedMenuItemAvailabilityDto
    {
        [Required(ErrorMessage = "Menu item ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid menu item ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Availability status is required")]
        public bool IsAvailable { get; set; }
    }
}
