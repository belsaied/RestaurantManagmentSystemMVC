using System.ComponentModel.DataAnnotations;

namespace Restaurant.PL.ViewModels.RecipeLineVM
{
    public class RecipeLineViewModel
    {
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 100000, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit is required.")]
        [StringLength(20, ErrorMessage = "Unit cannot exceed 20 characters.")]
        public string Unit { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingredient is required.")]
        [Display(Name = "Ingredient")]
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "Menu Item is required.")]
        [Display(Name = "Menu Item")]
        public int MenuId { get; set; }
    }
}
