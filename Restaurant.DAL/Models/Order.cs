using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Models
{
    public class Order : baseEntity
    {
        public int CustomerId { get; set; } //FK From Customer
        public int SubTotal { get; set; }
        public int ServiceTax { get; set; }
        public int Total { get; set; }
        public string PaymentStatus { get; set; } = null!;
        public int Discount { get; set; }
        public string OrderType { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int TableId { get; set; } //FK From Table

    }

}