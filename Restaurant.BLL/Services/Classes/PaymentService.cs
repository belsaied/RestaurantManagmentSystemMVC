using AutoMapper;
using Restaurant.BLL.DTOs.PaymentModule;
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
    public class PaymentService(IUnitOfWork _unitOfWork, IMapper _mapper) : IPaymentService
    {
        public IEnumerable<PaymentDto> GetAll(bool withTracking = false) =>
       _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentDto>>(_unitOfWork.PaymentRepository.GetAll(withTracking));

        public PaymentDetailsDto GetById(int id) =>
            _mapper.Map<Payment, PaymentDetailsDto>(_unitOfWork.PaymentRepository.GetById(id));

        public int Add(CreatedPaymentDto createdPayment)
        {
            _unitOfWork.PaymentRepository.Add(_mapper.Map<CreatedPaymentDto, Payment>(createdPayment));
            return _unitOfWork.SaveChanges();
        }
        public int Update(UpdatedPaymentDto updatedPayment)
        {
            _unitOfWork.PaymentRepository.Update(_mapper.Map<UpdatedPaymentDto, Payment>(updatedPayment));
            return _unitOfWork.SaveChanges();
        }
        public bool Delete(int id)
        { 
            _unitOfWork.PaymentRepository.DeleteById(id);
            return _unitOfWork.SaveChanges() > 0 ? true : false;
        }

    }
}
