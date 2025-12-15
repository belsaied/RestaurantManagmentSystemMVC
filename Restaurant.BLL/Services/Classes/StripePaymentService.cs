using Microsoft.Extensions.Options;
using Restaurant.BLL.DTOs.PaymentModule;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.BLL.Settings;
using Restaurant.DAL.Data.Repositories.Interfaces;
using Restaurant.DAL.Models;


namespace Restaurant.BLL.Services.Classes
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly StripeSettings _stripeSettings;
        private readonly IUnitOfWork _unitOfWork;

        public StripePaymentService(IOptions<StripeSettings> stripeSettings, IUnitOfWork unitOfWork)
        {
            _stripeSettings = stripeSettings.Value;
            _unitOfWork = unitOfWork;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        public async Task<PaymentIntentDto> CreatePaymentIntent(CreatePaymentDto createPaymentDto)
        {
            var order = _unitOfWork.OrderRepository.GetById(createPaymentDto.OrderId);
            if (order == null)
                throw new ArgumentException("Order not found");

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(createPaymentDto.Amount * 100), // Convert to cents
                Currency = createPaymentDto.Currency,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            // Save payment record
            var payment = new Payment
            {
                OrderId = createPaymentDto.OrderId,
                Amount = createPaymentDto.Amount,
                Currency = createPaymentDto.Currency,
                Status = "pending",
                StripePaymentIntentId = paymentIntent.Id,
                CreatedAt = DateTime.UtcNow
            };

            _unitOfWork.PaymentRepository.Add(payment);
            _unitOfWork.SaveChanges();

            return new PaymentIntentDto
            {
                ClientSecret = paymentIntent.ClientSecret,
                PaymentIntentId = paymentIntent.Id
            };
        }

        public async Task<bool> ConfirmPayment(string paymentIntentId)
        {
            var payment = _unitOfWork.PaymentRepository.GetAll()
                .FirstOrDefault(p => p.StripePaymentIntentId == paymentIntentId);

            if (payment == null)
                return false;

            var service = new PaymentIntentService();
            var paymentIntent = await service.GetAsync(paymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
                payment.Status = "succeeded";
                _unitOfWork.PaymentRepository.Update(payment);
                _unitOfWork.SaveChanges();
                return true;
            }

            return false;
        }

        public PaymentDto? GetPaymentByOrderId(int orderId)
        {
            var payment = _unitOfWork.PaymentRepository.GetAll()
                .FirstOrDefault(p => p.OrderId == orderId);

            if (payment == null)
                return null;

            return new PaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                Currency = payment.Currency,
                Status = payment.Status,
                StripePaymentIntentId = payment.StripePaymentIntentId,
                CreatedAt = payment.CreatedAt
            };
        }
    }
}