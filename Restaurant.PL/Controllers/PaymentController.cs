using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.DTOs.PaymentModule;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.PL.ViewModels.PaymentVM;

namespace Restaurant.PL.Controllers
{
    public class PaymentController(IPaymentServices _PayServe) : Controller
    {
        public IActionResult Index()
        {
            
            return View(_PayServe.GetAll());
        }
        public IActionResult Details(int Id)
        {
            return View(_PayServe.GetById(Id));
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
                return RedirectToAction(nameof(Index));
            }
            
                return View("Add", createdPaymentDto);
            
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var payment=_PayServe.GetById(Id);

            var PayViweModle = new PaymentUpdateViewModel()
            {
                Id = payment.Id,
                PaymentDate = payment.PaymentDate,
                Amount = payment.Amount,
                Status = payment.Status,
                TransactionRef = payment.TransactionRef,
                CreatedOn = payment.CreatedOn,
                ModifiedOn=DateTime.Now
                
            };

            //use View model
            return View(PayViweModle);
        }
        [HttpPost]
        public IActionResult Edit(PaymentUpdateViewModel ModelFromView)
        {
           var UpdatedPayment = new UpdatedPaymentDto()
           {
               Id  = ModelFromView.Id,
               PaymentDate=ModelFromView.PaymentDate,
               Amount=ModelFromView.Amount,
               Status =ModelFromView.Status,
               TransactionRef=ModelFromView.TransactionRef,
               CreatedOn = ModelFromView.CreatedOn,
               ModifiedOn=DateTime.Now
           };
            if (ModelState.IsValid)
            {
                _PayServe.Update(UpdatedPayment);
                return RedirectToAction(nameof(Index));
            }
            return View(UpdatedPayment);
        }

        public IActionResult Delete(int Id)
        {
            _PayServe.Delete(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
