using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.BLL.DTOs.OrderDTOs
{
    public class CreateOrderDTO
    {
        
        public int CustomerId { get; set; } // FK From Customer

        [Required(AllowEmptyStrings = false, ErrorMessage = "Payment status is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Payment status must be 3-20 characters")]
        [RegularExpression(@"^(Pending|Paid|Unpaid|Refunded|Failed)$",
            ErrorMessage = "Payment status must be one of: Pending, Paid, Unpaid, Refunded, Failed")]
        public string PaymentStatus { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Order type is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Order type must be 3-20 characters")]

        [RegularExpression(@"^(DineIn|TakeAway|Delivery|Pickup)$",
            ErrorMessage = "Order type must be one of: DineIn, TakeAway, Delivery, Pickup")]
        public string OrderType { get; set; } = null!;

        
        public int TableId { get; set; } // FK From Table

        
    }
}
