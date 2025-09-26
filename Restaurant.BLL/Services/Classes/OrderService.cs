using AutoMapper;
using Restaurant.BLL.DTOs.OrderDTO;
using Restaurant.BLL.DTOs.OrderDTOs;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.DAL.Data.Repositories.Interfaces;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services.Classes
{
    public class OrderService(IOrderRepository orderRepository, IMapper _mapper) : IOrderService
    {

        public IEnumerable<OrderDTO> GetAllOrders(bool WithTracking = false)
        {
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orderRepository.GetAllOrders(WithTracking));

        }

        public OrderDetailsDTO? GetOrderById(int Id)
        {
            return _mapper.Map<Order, OrderDetailsDTO>(orderRepository.GetOrderById(Id));
        }

        public int AddOrder(CreateOrderDTO orderDTO)
        {
            return orderRepository.AddOrder(_mapper.Map<CreateOrderDTO, Order>(orderDTO));
        }

        public int UpdateOrder(UpdateOrderDTO orderDTO)
        {
            return orderRepository.UpdateOrder(_mapper.Map<UpdateOrderDTO, Order>(orderDTO));

        }

        public int DeleteOrderById(int id)
        {
            return orderRepository.DeleteOrderById(id);
        }
    }
}
