namespace Restaurant.DAL.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        int AddOrder(Order order);
        int DeleteOrderById(int id);
        IEnumerable<Order>? GetAllOrders(bool WithTracking = false);
        Order? GetOrderById(int Id);
        int UpdateOrder(Order order);
    }
}