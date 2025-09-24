using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Models
{
    public class MenuItem:baseEntity
    {
        public string ItemName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        // Navigation Properties
        
        public  Category Category { get; set; } = null!;


        public virtual ICollection<RecipeLine> RecipeLines { get; set; } = new HashSet<RecipeLine>();

        public virtual ICollection<OrderItems> OrderItems { get; set; } = new HashSet<OrderItems>();
        //public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        //public virtual ICollection<RecipeLine> RecipeLines { get; set; } = new HashSet<RecipeLine>();
        //              -----------until the 2 tables are created----------------- 
    }
}
