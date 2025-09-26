using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Models
{
    public class RecipeLine
    {
        public int Id { get; set; }
        
        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(IngredientId))]
        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; } = null!;

        [ForeignKey(nameof(RecipeId))]
        public int RecipeId { get; set; }


        public MenuItem MenuItem { get; set; } = null!;

    
    }
}
