using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Repositories.Classes
{
    public class PaymentRepository(AppDbContext dbContext) : IPaymentRepository
    {
        public IEnumerable<Payment> GetAll(bool withTracking = false) =>
            withTracking
            ? dbContext.Payments.Where(p => !p.IsDeleted).ToList()
            : dbContext.Payments.AsNoTracking().Where(p => !p.IsDeleted).ToList();

        public Payment? GetById(int id) =>
            dbContext.Payments.FirstOrDefault(p => p.Id == id);

        public int Add(Payment payment)
        {
            dbContext.Add(payment);
            return dbContext.SaveChanges();
        }

        public int Update(Payment payment)
        {
            dbContext.Update(payment);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var payment = GetById(id);
            if (payment != null)
            {
                payment.IsDeleted = true;
                Update(payment);
            }
            return 0;
        }
    }

}
