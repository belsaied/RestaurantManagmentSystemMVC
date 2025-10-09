using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.RecipeLinesDtos
{
    public class UpdatedRecipeDto
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int IngredientId { get; set; }
        public int MenuId { get; set; }
    }
}
