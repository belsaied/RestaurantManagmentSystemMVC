using System.ComponentModel.DataAnnotations;

namespace Restaurant.PL.ViewModels.IngredientVM
{
    public class IngredientViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Ingredient Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Unit is required.")]
        [StringLength(20, ErrorMessage = "Unit cannot exceed 20 characters.")]
        [Display(Name = "Unit")]
        public string Unit { get; set; } = string.Empty;

        [Required(ErrorMessage = "Unit Cost is required.")]
        [Range(0.01, 10000, ErrorMessage = "Unit Cost must be between 0.01 and 10,000.")]
        [Display(Name = "Unit Cost")]
        [DataType(DataType.Currency)]
        public decimal UnitCost { get; set; }

        [Required(ErrorMessage = "Current Stock is required.")]
        [Range(0, 100000, ErrorMessage = "Current Stock must be between 0 and 100,000.")]
        [Display(Name = "Current Stock")]
        public decimal CurrentStock { get; set; }

        [Required(ErrorMessage = "Minimum Stock is required.")]
        [Range(0, 100000, ErrorMessage = "Minimum Stock must be between 0 and 100,000.")]
        [Display(Name = "Minimum Stock")]
        public decimal MinStock { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        
    }
}
