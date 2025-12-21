using System.ComponentModel.DataAnnotations;

namespace Restaurant.BLL.DTOs.RecipeLinesDtos
{
    public class UpdateRecipeLineDto:RecipeLineBaseDto
    {
        [Required(ErrorMessage = "Recipe Line ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid recipe line ID")]
        public int Id { get; set; }
    }
}
