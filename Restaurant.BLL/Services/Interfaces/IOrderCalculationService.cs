using Restaurant.DAL.Models;

namespace Restaurant.BLL.Services.Interfaces
{
    public interface IOrderCalculationService
    {
        void CalculateOrderTotals(Order order);
    }
}
