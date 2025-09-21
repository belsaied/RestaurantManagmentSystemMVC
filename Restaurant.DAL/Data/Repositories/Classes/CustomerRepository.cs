using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Repositories.Classes
{
    public class CustomerRepository(AppDbContext _dbContext) : ICustomerRepository
    {
        public IEnumerable<Customer>? GetAllCustomers(bool WithTracking = false)
        {
            return WithTracking ? _dbContext.Customers.ToList()
                : _dbContext.Customers.AsNoTracking().ToList();

        }
        public Customer? GetCustomerById(int Id)
        {
            return _dbContext.Customers.Find(Id);

        }

        public int AddCustomer(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            return _dbContext.SaveChanges();

        }

        public int UpdateCustomer(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            return _dbContext.SaveChanges();
        }

        public int DeleteCustomerById(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                return _dbContext.SaveChanges();
            }
            else
                return 0;


        }



    }
}
