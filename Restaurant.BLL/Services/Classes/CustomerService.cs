using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Restaurant.BLL.AttachmentService;
using Restaurant.BLL.DTOs.CustomerDTOs;
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
    public class CustomerService(IUnitOfWork _unitOfWork, IMapper _mapper,IAttachmentService _attachmentService) : ICustomerService
    {
        public IEnumerable<CustomerDTO> GetAllCustomers(bool WithTracking = false)
        {
            var customers = _unitOfWork.CustomerRepository.GetAll(WithTracking);
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }

        public CustomerDetailsDTO? GetCustomerById(int Id)
        {
            var customer = _unitOfWork.CustomerRepository.GetById(Id);
            return _mapper.Map<Customer, CustomerDetailsDTO>(customer);
        }

        public int AddCustomer(CreateCustomerDTO customerDTO)
        {
            if (customerDTO is not null)
            {
                var customer = _mapper.Map<CreateCustomerDTO, Customer>(customerDTO);
                if(customerDTO.Image is not null)
                    customer.ImageName = _attachmentService.Upload(customerDTO.Image,"Images");

                _unitOfWork.CustomerRepository.Add(customer);
                return _unitOfWork.SaveChanges();
            }
            else
            {
                return 0;
            }

        }

        public int UpdateCustomer(UpdateCustomerDTO customerDTO)
        {
            if (customerDTO is not null)
            {
                 _unitOfWork.CustomerRepository.Update(_mapper.Map<UpdateCustomerDTO, Customer>(customerDTO));
               return _unitOfWork.SaveChanges();
            }
            return 0;

        }

        public bool DeleteCustomer(int Id)
        {
            var customer = _unitOfWork.CustomerRepository.GetById(Id);
            if (customer is not null)
            {
                _unitOfWork.CustomerRepository.DeleteById(Id);
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;

        }

    }
}
