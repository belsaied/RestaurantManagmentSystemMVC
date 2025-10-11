using System.ComponentModel.DataAnnotations;

namespace Restaurant.PL.ViewModels.MenuItemVM
{
    public class MenuItemsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item Name is required")]
        [MaxLength(100, ErrorMessage = "Max length should be 100 characters")]
        [MinLength(3, ErrorMessage = "Min length should be 3 characters")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; } = null!;

        [MaxLength(500, ErrorMessage = "Max length should be 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Menu Item Image")]
        public IFormFile? Image { get; set; }

        public string? ImageName { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
