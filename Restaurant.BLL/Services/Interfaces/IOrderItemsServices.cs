using Restaurant.BLL.DTOs.OrderItemsModule;

namespace Restaurant.BLL.Services.Interfaces
{
    public interface IOrderItemsServices
    {
        int Add(CreatedOrderItems createdOrderItems);
        bool Delete(int id);
        IEnumerable<OrderItemDto> GetAll(bool WithTracking = false);
        OrderItemsDetailsDto GetById(int id);
        int Update(UpdatedOrderItems updatedOrderItems);
    }
}