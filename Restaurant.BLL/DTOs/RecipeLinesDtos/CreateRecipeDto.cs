using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.RecipeLinesDtos
{
    public class CreateRecipeDto
    {
      

        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;

        [Display(Name = "Ingredient")]
        public int IngredientId { get; set; }
        [Display(Name = "Menu Item")]
        public int MenuId { get; set; }



    }
}
