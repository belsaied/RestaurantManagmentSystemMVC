using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.RecipeLinesDtos
{
    public class RecipesDetailsDto
    {
        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;




        public string Ingredient { get; set; } = null!;


        public string MenuItem { get; set; } = null!;

        public int IngredientId { get; set; }
        public int MenuId { get; set; }

        public int Id { get; set; }
        public string? CreatedBy { get; set; }   // UserId
        public DateTime CreatedOn { get; set; }   // the DateTime of Creating the Record. (Nullable because i'll put a default value for him in the configurations).
        public string? ModifiedBy { get; set; }   // UserId
        public DateTime ModifiedOn { get; set; }  // the DateTime of Modifying the Record. (Nullable because i'll put a default value for him in the configurations).
        

        }
}
