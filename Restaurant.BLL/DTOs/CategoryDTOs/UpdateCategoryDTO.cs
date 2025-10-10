using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.CategoryDTOs
{
    public class UpdateCategoryDTO:CategoryBaseDTO
    {
        [Required(ErrorMessage = "Category ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid category ID")]
        public int Id { get; set; }
        
        public string? ImageName { get; set; }
    }
}
