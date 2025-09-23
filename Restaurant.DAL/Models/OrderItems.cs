using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Models
{
    public class OrderItems:baseEntity
    {
        public int Quantity { get; set; } 

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage ="*")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Totla Price")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Special Instructions")]

        public string? SpecialInstructions { get; set; }

       
    }

}
