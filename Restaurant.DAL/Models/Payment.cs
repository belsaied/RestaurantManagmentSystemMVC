using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Models
{
    public class Payment:baseEntity
    {
        [Required(ErrorMessage ="*")]
        public decimal Amount { get; set; }


        [Required(ErrorMessage ="*")]
        [Display(Name ="Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage ="*")]
        [Display(Name ="Transaction Reference")]
        public string TransactionRef { get; set; } = null!;
        public string? Status { get; set; }

    }
}
