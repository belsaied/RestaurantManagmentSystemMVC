using System.ComponentModel.DataAnnotations;

namespace Restaurant.PL.ViewModels
{
    public class PaymentUpdateViewModel
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

        public string? Status { get; set; }
    }
}
