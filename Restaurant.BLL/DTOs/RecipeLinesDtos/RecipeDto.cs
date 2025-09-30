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
    public class RecipeDto
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;

        [Display(Name = "Ingredient")]
        public string IngredientName { get; set; } = string.Empty;
        [Display(Name = "Menu Item")]
        public string MenuItemName { get; set; } = string.Empty;
        
        public int IngredientId { get; set; }
        public int MenuId { get; set; }
    }
}
