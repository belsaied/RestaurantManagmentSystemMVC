using Restaurant.BLL.DTOs.PaymentModule;

namespace Restaurant.BLL.Services.Interfaces
{
    public interface IPaymentServices
    {
        int Add(CreatedPaymentDto createdPayment);
        bool Delete(int id);
        IEnumerable<PaymentDto> GetAll(bool withTracking = false);
        PaymentDetailsDto GetById(int id);
        int Update(UpdatedPaymentDto updatedPayment);
    }
}