using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Repositories.Classes
{
    public class OrderRepository(AppDbContext _dbContext) : IOrderRepository
    {
        public IEnumerable<Order>? GetAllOrders(bool WithTracking = false)
        {
            return WithTracking ? _dbContext.Orders.ToList()
                : _dbContext.Orders.AsNoTracking().ToList();

        }
        public Order? GetOrderById(int Id)
        {
            return _dbContext.Orders.Find(Id);

        }

        public int AddOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            return _dbContext.SaveChanges();

        }

        public int UpdateOrder(Order order)
        {
            _dbContext.Orders.Update(order);
            return _dbContext.SaveChanges();
        }

        public int DeleteOrderById(int id)
        {
            var order = _dbContext.Orders.Find(id);
            if (order != null)
            {
                order.IsDeleted = true;
                _dbContext.Orders.Update(order);
                return _dbContext.SaveChanges();
            }
            else
                return 0;
        }

    }
}