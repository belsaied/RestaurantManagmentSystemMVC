using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.RecipeLinesDtos
{
    internal class CreateRecipeDto
    {
      

        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
    }
}
