using Restaurant.BLL.DTOs.CustomerDTOs;

namespace Restaurant.BLL.Services.Interfaces
{
    public interface ICustomerService
    {
        int AddCustomer(CreateCustomerDTO customerDTO);
        bool DeleteCustomer(int Id);
        IEnumerable<CustomerDTO> GetAllCustomers(bool WithTracking = false);
        CustomerDetailsDTO? GetCustomerById(int Id);
        int UpdateCustomer(UpdateCustomerDTO customerDTO);
    }
}