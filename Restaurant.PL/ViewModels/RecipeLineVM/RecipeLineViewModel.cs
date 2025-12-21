using System.ComponentModel.DataAnnotations;

namespace Restaurant.PL.ViewModels.RecipeLineVM
{
    public class RecipeLineViewModel
    {
        #region Old.
        //[Required(ErrorMessage = "Quantity is required.")]
        //[Range(1, 100000, ErrorMessage = "Quantity must be at least 1.")]
        //public int Quantity { get; set; }

        //[Required(ErrorMessage = "Unit is required.")]
        //[StringLength(20, ErrorMessage = "Unit cannot exceed 20 characters.")]
        //public string Unit { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Ingredient is required.")]
        //[Display(Name = "Ingredient")]
        //public int IngredientId { get; set; }

        //[Required(ErrorMessage = "Menu Item is required.")]
        //[Display(Name = "Menu Item")]
        //public int MenuId { get; set; } 
        #endregion

        public int Id { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0.01, 999999, ErrorMessage = "Quantity must be between 0.01 and 999,999")]
        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Unit is required")]
        [StringLength(50, ErrorMessage = "Unit cannot exceed 50 characters")]
        [Display(Name = "Unit of Measurement")]
        public string Unit { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingredient is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid ingredient")]
        [Display(Name = "Ingredient")]
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "Menu Item is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid menu item")]
        [Display(Name = "Menu Item")]
        public int MenuId { get; set; }

        // For display purposes
        public string? IngredientName { get; set; }
        public string? MenuItemName { get; set; }
    }
}
