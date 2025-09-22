namespace Restaurant.DAL.Data.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        int Add(OrderItems orderItem);
        int Delete(int id);
        IEnumerable<OrderItems> GetAll(bool withTracking = false);
        OrderItems? GetById(int id);
        int Update(OrderItems orderItem);
    }
}