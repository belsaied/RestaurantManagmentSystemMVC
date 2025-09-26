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
            return WithTracking ? _dbContext.Customers.ToList().Where(c=>c.IsDeleted==false)
                : _dbContext.Customers.AsNoTracking().ToList().Where(c => c.IsDeleted == false);

        }
        public Customer? GetCustomerById(int Id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == Id);
            return customer.IsDeleted==false &&customer!=null? customer :null;

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

        public int DeleteCustomerById(int Id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c=>c.Id==Id);
            if (customer is not null && customer.IsDeleted==false)
            {
                customer.IsDeleted = true;
                return _dbContext.SaveChanges();
            }
            return 0;


        }



    }
}
