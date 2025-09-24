using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.CategoryDTOs
{
    public class CategoryBaseDTO
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters")]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Display order is required")]
        [Range(1, 999, ErrorMessage = "Display order must be between 1 and 999")]
        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
