using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.PaymentModule
{
    public class UpdatedPaymentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        public decimal Amount { get; set; }


        [Required(ErrorMessage = "*")]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Transaction Reference")]
        public string TransactionRef { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string? Status { get; set; }
    }
}
