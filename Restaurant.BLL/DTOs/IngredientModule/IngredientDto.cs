using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.IngredientModule
{
    public class IngredientDto
    {
        public int Id { get; set; }
               
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;

        
        [DataType(DataType.Currency)]
        [Display(Name = "Unit Cost")]

        public decimal UnitCost { get; set; }
        [Display(Name = "Current Stock")]

        public decimal CurrentStock { get; set; }
        [Display(Name = "Min Stock")]

        public decimal MinStock { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
