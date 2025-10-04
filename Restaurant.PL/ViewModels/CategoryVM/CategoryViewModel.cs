using System.ComponentModel.DataAnnotations;

namespace Restaurant.PL.ViewModels.CategoryVM
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name Can't Be Null")]
        [MaxLength(50, ErrorMessage = "Max length should be 50 characters")]
        [MinLength(3, ErrorMessage = "Min length should be 3 characters")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "Max length should be 200 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Display Order is required")]
        [Range(1, 1000, ErrorMessage = "Display Order must be between 1 and 1000")]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}
