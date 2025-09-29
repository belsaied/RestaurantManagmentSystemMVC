using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.DTOs.PaymentModule;
using Restaurant.BLL.Services.Interfaces;

namespace Restaurant.PL.Controllers
{
    public class PaymentController(IPaymentServices _PayServe) : Controller
    {
        public IActionResult Index()
        {
            
            return View(_PayServe.GetAll());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CreatedPaymentDto createdPaymentDto)
        {
            if (ModelState.IsValid)
            {
                _PayServe.Add(createdPaymentDto);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
