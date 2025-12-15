using Restaurant.BLL.DTOs.PaymentModule;

namespace Restaurant.BLL.Services.Interfaces
{
    public interface IStripePaymentService
    {
        Task<PaymentIntentDto> CreatePaymentIntent(CreatePaymentDto createPaymentDto);
        Task<bool> ConfirmPayment(string paymentIntentId);
        PaymentDto? GetPaymentByOrderId(int orderId);
    }
}
