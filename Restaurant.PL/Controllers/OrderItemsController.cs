using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.DTOs.OrderItemsModule;
using Restaurant.BLL.Services.Interfaces;

namespace Restaurant.PL.Controllers
{
    [Authorize]

    public class OrderItemsController(IOrderItemsServices _OrderItmesService) : Controller
    {
        public IActionResult Index()
        {
            return View(_OrderItmesService.GetAll());
        }
        public IActionResult Details(int Id)
        {
            return View(_OrderItmesService.GetById(Id));  
        }
        [HttpGet]

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CreatedOrderItems createdOrderItems)
        {
            if (ModelState.IsValid)
            {
                _OrderItmesService.Add(createdOrderItems);
                return RedirectToAction(nameof(Index));
            }
            return View(createdOrderItems);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
          var OrderItem=  _OrderItmesService.GetById(Id);
            var ToBeUpdatedOrderItemDto = new UpdatedOrderItems()
            {
                Id = OrderItem.Id,
                Quantity=OrderItem.Quantity,
                UnitPrice=OrderItem.UnitPrice,
                TotalPrice=OrderItem.TotalPrice,
                SpecialInstructions=OrderItem.SpecialInstructions,
                CreatedOn= OrderItem.CreatedOn,
                ModifiedOn=DateTime.Now
            };
            return View(ToBeUpdatedOrderItemDto);
        }
        [HttpPost]
        public IActionResult Edit(UpdatedOrderItems updatedOrderItems)
        {
            if (ModelState.IsValid)
            {
                _OrderItmesService.Update(updatedOrderItems);
                return RedirectToAction(nameof(Index));
            }
            return View(updatedOrderItems);
        }
        public IActionResult Delete(int Id)
        {
            _OrderItmesService.Delete(Id);
            return RedirectToAction(nameof(Index));
        }

    }
}
