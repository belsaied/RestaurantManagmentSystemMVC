using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Models
{
    public class Ingredient:baseEntity
    {
        
        [Required]
        public string Name { get; set; }=string.Empty;
        public string Unit { get; set; }= string.Empty;
        [Range(0, double.MaxValue)]

        public decimal UnitCost { get; set; }

        public decimal CurrentStock { get; set; }
        public decimal MinStock { get; set; }

        public bool IsActive { get; set; }
        public string? ImageName { get; set; }
        public ICollection<RecipeLine> RecipeLines { get; set; } = new HashSet<RecipeLine>();
    }
}
