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
    public class PaymentServices(IPaymentRepository _paymentRepo, IMapper _mapper) : IPaymentServices
    {
        public IEnumerable<PaymentDto> GetAll(bool withTracking = false) =>
       _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentDto>>(_paymentRepo.GetAll(withTracking));

        public PaymentDetailsDto GetById(int id) =>
            _mapper.Map<Payment, PaymentDetailsDto>(_paymentRepo.GetById(id));

        public int Add(CreatedPaymentDto createdPayment) =>
            _paymentRepo.Add(_mapper.Map<CreatedPaymentDto, Payment>(createdPayment));

        public int Update(UpdatedPaymentDto updatedPayment) =>
            _paymentRepo.Update(_mapper.Map<UpdatedPaymentDto, Payment>(updatedPayment));

        public bool Delete(int id) => _paymentRepo.Delete(id) > 0;

    }
}
