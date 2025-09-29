using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Models
{
    public class RecipeLine: baseEntity
    {
        public int Id { get; set; }
        
        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;

        

        [ForeignKey(nameof(Ingredient))]
        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; } = null!;

        [ForeignKey(nameof(MenuItem))]
        public int MenuId { get; set; }


        public MenuItem MenuItem { get; set; } = null!;

    
    }
}
