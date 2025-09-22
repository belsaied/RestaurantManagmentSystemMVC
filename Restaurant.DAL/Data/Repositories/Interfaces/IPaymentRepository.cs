namespace Restaurant.DAL.Data.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        int Add(Payment payment);
        int Delete(int id);
        IEnumerable<Payment> GetAll(bool withTracking = false);
        Payment? GetById(int id);
        int Update(Payment payment);
    }
}