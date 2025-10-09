using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Repositories.Classes
{
    public class CustomerRepository(AppDbContext _dbContext) : GenericRepository<Customer>(_dbContext), ICustomerRepository
    {
        



    }
}
