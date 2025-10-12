using Humanizer.Localisation.Ordinalizers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.DTOs.OrderDTOs;
using Restaurant.BLL.Services.Interfaces;

namespace Restaurant.PL.Controllers
{
    [Authorize]

    public class OrderController(IOrderService _orderService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(_orderService.GetAllOrders());
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            return View(_orderService.GetOrderById(Id));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CreateOrderDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                _orderService.AddOrder(orderDTO);
                return RedirectToAction("Index");
            }
            return View(orderDTO);
        }

        [HttpGet]
        public IActionResult Update(int Id)
        {
            var order = _orderService.GetOrderById(Id);
            UpdateOrderDTO updateOrderDTO = new UpdateOrderDTO()
            {
                PaymentStatus = order.PaymentStatus,
                OrderType = order.OrderType,
                Id = order.Id,
                CustomerId = order.CustomerId,
                TableId = order.TableId
            };
            return View(updateOrderDTO);

        }

        [HttpPost]
        public IActionResult Update(UpdateOrderDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                _orderService.UpdateOrder(orderDTO);
                return RedirectToAction("Index");
            }
            return View(orderDTO);
        }


        public IActionResult Delete(int Id)
        {
            _orderService.DeleteOrderById(Id);
            return RedirectToAction("Index");
        }






    }
}
