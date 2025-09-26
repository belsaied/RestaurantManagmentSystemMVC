using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.PaymentModule
{
    public class PaymentDto
    {
        public decimal Amount { get; set; }

        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Transaction Reference")]
        public string TransactionRef { get; set; } = null!;
    }
}
