namespace Restaurant.DAL.Models
{
    public class Order : baseEntity
    {
        public int? CustomerId { get; set; }
        public decimal SubTotal { get; set; }  // Changed from int
        public decimal ServiceTax { get; set; } // Changed from int
        public decimal Total { get; set; }      // Changed from int
        public string? PaymentStatus { get; set; }
        public decimal Discount { get; set; }   // Changed from int
        public string? OrderType { get; set; }
        public string? Status { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int? TableId { get; set; }

        public Table? NavTable { get; set; }
        public Customer? NavCustomer { get; set; }
        public ICollection<Payment>? NavPayments { get; set; }
        public ICollection<OrderItems> NavOrderItems { get; set; } = null!;

    }

}