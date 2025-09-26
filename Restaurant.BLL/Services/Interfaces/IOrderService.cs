using Restaurant.BLL.DTOs.OrderDTO;
using Restaurant.BLL.DTOs.OrderDTOs;

namespace Restaurant.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(CreateOrderDTO orderDTO);
        int DeleteOrderById(int id);
        IEnumerable<OrderDTO> GetAllOrders(bool WithTracking = false);
        OrderDetailsDTO? GetOrderById(int Id);
        int UpdateOrder(UpdateOrderDTO orderDTO);
    }
}