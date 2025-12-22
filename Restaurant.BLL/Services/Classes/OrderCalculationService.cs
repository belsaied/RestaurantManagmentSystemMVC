using Restaurant.BLL.Services.Interfaces;
using Restaurant.DAL.Models;

namespace Restaurant.BLL.Services.Classes
{
    public class OrderCalculationService : IOrderCalculationService
    {
        public void CalculateOrderTotals(Order order)
        {
            if (order.NavOrderItems == null || !order.NavOrderItems.Any())
            {
                order.SubTotal = 0;
                order.ServiceTax = 0;
                order.Total = 0;
                return;
            }

            // Calculate each OrderItem's TotalPrice
            foreach (var item in order.NavOrderItems)
            {
                item.TotalPrice = item.UnitPrice * item.Quantity;
            }

            // Calculate Order totals
            order.SubTotal = order.NavOrderItems.Sum(oi => oi.TotalPrice);
            order.ServiceTax = order.SubTotal * 0.10m; // 10% service tax
            order.Total = order.SubTotal + order.ServiceTax - order.Discount;
        }
    }
}
