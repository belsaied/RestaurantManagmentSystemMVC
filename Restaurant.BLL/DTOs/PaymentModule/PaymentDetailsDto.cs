using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.PaymentModule
{
    public class PaymentDetailsDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal Amount { get; set; }


        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Transaction Reference")]
        public string TransactionRef { get; set; } = null!;
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }   // UserId
        public DateTime? CreatedOn { get; set; }   // Date of Creation
    }
}
