using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.IngredientModule
{
    public class IngredientDetailsDto
    {
        public int Id { get; set; }
        public string? CreatedBy { get; set; }   // UserId
        public DateTime CreatedOn { get; set; }   // the DateTime of Creating the Record. (Nullable because i'll put a default value for him in the configurations).
        public string? ModifiedBy { get; set; }   // UserId
        public DateTime ModifiedOn { get; set; }  // the DateTime of Modifying the Record. (Nullable because i'll put a default value for him in the configurations).

        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;

        public decimal UnitCost { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal MinStock { get; set; }

        public bool IsActive { get; set; }
        public string? Image { get; set; }
    }
}
