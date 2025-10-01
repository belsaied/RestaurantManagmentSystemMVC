using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.OrderItemsModule
{
    public class UpdatedOrderItems
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "*")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Totla Price")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Special Instructions")]

        public string? SpecialInstructions { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }= DateTime.Now;
    }
}
