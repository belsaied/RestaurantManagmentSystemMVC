using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.Json;
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
    public class CustomerService(ICustomerRepository _customerRepository, IMapper _mapper) : ICustomerService
    {
        public IEnumerable<CustomerDTO> GetAllCustomers(bool WithTracking = false)
        {
            var customers = _customerRepository.GetAll(WithTracking);
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }

        public CustomerDetailsDTO? GetCustomerById(int Id)
        {
            var customer = _customerRepository.GetById(Id);
            return _mapper.Map<Customer, CustomerDetailsDTO>(customer);
        }

        public int AddCustomer(CreateCustomerDTO customerDTO)
        {
            if (customerDTO is not null)
            {
                var customer = _mapper.Map<CreateCustomerDTO, Customer>(customerDTO);
                return _customerRepository.Add(customer);
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
                return _customerRepository.Update(_mapper.Map<UpdateCustomerDTO, Customer>(customerDTO));
            }
            return 0;

        }

        public bool DeleteCustomer(int Id)
        {
            var customer = _customerRepository.GetById(Id);
            if (customer is not null)
            {
                _customerRepository.DeleteById(Id);
                return true;
            }
            return false;


        }

    }
}
