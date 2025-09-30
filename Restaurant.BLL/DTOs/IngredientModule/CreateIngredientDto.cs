using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.IngredientModule
{
    public class CreateIngredientDto

    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;


        public decimal UnitCost { get; set; }
       

        public decimal CurrentStock { get; set; }
       

        public decimal MinStock { get; set; }
      
        public bool IsActive { get; set; }
    }
}
