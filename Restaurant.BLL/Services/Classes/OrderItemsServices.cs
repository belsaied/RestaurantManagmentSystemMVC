using AutoMapper;
using Restaurant.BLL.DTOs.OrderItemsModule;
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
    public class OrderItemsServices(IOrderItemRepository _orderItemRepo, IMapper _mapper) : IOrderItemsServices
    {
        public IEnumerable<OrderItemDto> GetAll(bool WithTracking = false) =>
        _mapper.Map<IEnumerable<OrderItems>, IEnumerable<OrderItemDto>>(_orderItemRepo.GetAll(WithTracking));

        public OrderItemsDetailsDto GetById(int id) =>
              _mapper.Map<OrderItems, OrderItemsDetailsDto>(_orderItemRepo.GetById(id));

        public int Add(CreatedOrderItems createdOrderItems) =>
            _orderItemRepo.Add(_mapper.Map<CreatedOrderItems, OrderItems>(createdOrderItems));

        public int Update(UpdatedOrderItems updatedOrderItems) =>
            _orderItemRepo.Update(_mapper.Map<UpdatedOrderItems, OrderItems>(updatedOrderItems));

        public bool Delete(int id) => _orderItemRepo.DeleteById(id) > 0;



    }
}
