using System.ComponentModel.DataAnnotations;

namespace Restaurant.BLL.DTOs.RecipeLinesDtos
{
    public class RecipeLineBaseDto
    {
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0.01, 999999, ErrorMessage = "Quantity must be between 0.01 and 999,999")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Unit is required")]
        [StringLength(50, ErrorMessage = "Unit cannot exceed 50 characters")]
        public string Unit { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingredient is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid ingredient")]
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "Menu Item is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid menu item")]
        public int MenuId { get; set; }
    }
}
