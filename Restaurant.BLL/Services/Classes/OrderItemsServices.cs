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
    public class OrderItemsServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IOrderItemsServices
    {
        public IEnumerable<OrderItemDto> GetAll(bool WithTracking = false) =>
        _mapper.Map<IEnumerable<OrderItems>, IEnumerable<OrderItemDto>>(_unitOfWork.OrderItemRepository.GetAll(WithTracking));

        public OrderItemsDetailsDto GetById(int id) =>
              _mapper.Map<OrderItems, OrderItemsDetailsDto>(_unitOfWork.OrderItemRepository.GetById(id));

        public int Add(CreatedOrderItems createdOrderItems)
        {
            _unitOfWork.OrderItemRepository.Add(_mapper.Map<CreatedOrderItems, OrderItems>(createdOrderItems));
            return _unitOfWork.SaveChanges();
        }
        public int Update(UpdatedOrderItems updatedOrderItems)
        { 
            _unitOfWork.OrderItemRepository.Update(_mapper.Map<UpdatedOrderItems, OrderItems>(updatedOrderItems));
        return _unitOfWork.SaveChanges();
        }
        public bool Delete(int id)
        {
            _unitOfWork.OrderItemRepository.DeleteById(id);
            return _unitOfWork.SaveChanges()>0? true:false;
        }



    }
}
