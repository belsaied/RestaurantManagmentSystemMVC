namespace Restaurant.DAL.Models
{
    public class Customer:baseEntity
    {
       
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int LoyaltyPoints { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Order>? NavOrders { get; set; }
    }
}