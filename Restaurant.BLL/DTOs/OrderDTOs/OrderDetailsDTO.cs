namespace Restaurant.BLL.DTOs.OrderDTOs
{
    public class OrderDetailsDTO
    {
        public int CustomerId { get; set; }
        public decimal SubTotal { get; set; }     // Changed from int
        public decimal ServiceTax { get; set; }   // Changed from int
        public decimal Total { get; set; }        // Changed from int
        public string PaymentStatus { get; set; } = null!;
        public decimal Discount { get; set; }     // Changed from int
        public string OrderType { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int TableId { get; set; }
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }


    }
}
