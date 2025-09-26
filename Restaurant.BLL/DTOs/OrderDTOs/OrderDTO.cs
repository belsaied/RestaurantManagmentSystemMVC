using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.OrderDTO
{
    public class OrderDTO
    {
        public int CustomerId { get; set; } //FK From Customer
        public int ServiceTax { get; set; }
        public int Total { get; set; }
        public string PaymentStatus { get; set; } = null!;
        public string OrderType { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int TableId { get; set; } //FK From Table
        public int Id { get; set; }
        public string? CreatedBy { get; set; }   
        public DateTime? CreatedOn { get; set; }   

    }
}
