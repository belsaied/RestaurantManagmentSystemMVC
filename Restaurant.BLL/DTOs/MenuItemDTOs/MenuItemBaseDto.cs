using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.MenuItemDTOs
{
    public class MenuItemBaseDto
    {
        [Required(ErrorMessage = "Item name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Item name must be between 2 and 100 characters")]
        public string ItemName { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 9999.99, ErrorMessage = "Price must be between 0.01 and 9999.99")]
        public decimal Price { get; set; }

        [StringLength(255, ErrorMessage = "Image URL cannot exceed 255 characters")]
        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid category ID")]
        public int CategoryId { get; set; }
    }
}
