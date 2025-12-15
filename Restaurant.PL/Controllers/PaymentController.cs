using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Restaurant.BLL.DTOs.PaymentModule;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.BLL.Settings;

namespace Restaurant.PL.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IStripePaymentService _paymentService;
        private readonly StripeSettings _stripeSettings;

        public PaymentController(IStripePaymentService paymentService, IOptions<StripeSettings> stripeSettings)
        {
            _paymentService = paymentService;
            _stripeSettings = stripeSettings.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProcessPayment(int orderId, decimal amount)
        {
            ViewBag.OrderId = orderId;
            ViewBag.Amount = amount;
            ViewBag.StripePublishableKey = _stripeSettings.PublishableKey;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentDto createPaymentDto)
        {
            try
            {
                var paymentIntent = await _paymentService.CreatePaymentIntent(createPaymentDto);
                return Json(new { clientSecret = paymentIntent.ClientSecret });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPayment([FromBody] string paymentIntentId)
        {
            try
            {
                var success = await _paymentService.ConfirmPayment(paymentIntentId);
                if (success)
                {
                    TempData["Message"] = "Payment successful!";
                    TempData["MessageType"] = "success";
                    return Json(new { success = true });
                }
                return BadRequest(new { error = "Payment confirmation failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult PaymentStatus(int orderId)
        {
            var payment = _paymentService.GetPaymentByOrderId(orderId);
            return View(payment);
        }
    }
}
