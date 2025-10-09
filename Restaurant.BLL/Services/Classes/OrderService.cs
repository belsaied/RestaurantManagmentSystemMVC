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
    public class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper) : IOrderService
    {

        public IEnumerable<OrderDTO> GetAllOrders(bool WithTracking = false)
        {
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(_unitOfWork.OrderRepository.GetAll(WithTracking));

        }

        public OrderDetailsDTO? GetOrderById(int Id)
        {
            return _mapper.Map<Order, OrderDetailsDTO>(_unitOfWork.OrderRepository.GetById(Id));
        }

        public int AddOrder(CreateOrderDTO orderDTO)
        {
             _unitOfWork.OrderRepository.Add(_mapper.Map<CreateOrderDTO, Order>(orderDTO));
            return _unitOfWork.SaveChanges();
        }

        public int UpdateOrder(UpdateOrderDTO orderDTO)
        {
             _unitOfWork.OrderRepository.Update(_mapper.Map<UpdateOrderDTO, Order>(orderDTO));
            return _unitOfWork.SaveChanges();

        }

        public int DeleteOrderById(int id)
        {
             _unitOfWork.OrderRepository.DeleteById(id);
            return _unitOfWork.SaveChanges();
        }
    }
}
