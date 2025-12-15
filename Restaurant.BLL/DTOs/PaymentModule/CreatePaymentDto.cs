using System.ComponentModel.DataAnnotations;

namespace Restaurant.BLL.DTOs.PaymentModule
{
    public class CreatePaymentDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        public string Currency { get; set; } = "usd";
    }
}
