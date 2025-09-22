using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Interfaces;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Repositories.Classes
{
    public class OrderItemRepository(AppDbContext dbContext) : IOrderItemRepository
    {
        public IEnumerable<OrderItems> GetAll(bool withTracking = false) =>
            withTracking
            ? dbContext.OrderItems.Where(o => !o.IsDeleted).ToList()
            : dbContext.OrderItems.AsNoTracking().Where(o => !o.IsDeleted).ToList();

        public OrderItems? GetById(int id) =>
            dbContext.OrderItems.FirstOrDefault(o => o.Id == id);

        public int Add(OrderItems orderItem)
        {
            dbContext.Add(orderItem);
            return dbContext.SaveChanges();
        }

        public int Update(OrderItems orderItem)
        {
            dbContext.Update(orderItem);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var orderItem = GetById(id);
            if (orderItem != null)
            {
                orderItem.IsDeleted = true;
                Update(orderItem);
            }
            return 0;
        }
    }

}
