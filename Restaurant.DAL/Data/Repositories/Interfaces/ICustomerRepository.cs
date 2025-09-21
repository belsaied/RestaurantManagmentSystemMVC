namespace Restaurant.DAL.Data.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        int AddCustomer(Customer customer);
        int DeleteCustomerById(int id);
        IEnumerable<Customer>? GetAllCustomers(bool WithTracking = false);
        Customer? GetCustomerById(int Id);
        int UpdateCustomer(Customer customer);
    }
}