using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.DTOs.OrderDTO;
using Restaurant.BLL.DTOs.OrderDTOs;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.DAL.Data.Contexts;

namespace Restaurant.PL.Controllers
{
    [Authorize]

    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly AppDbContext _context;

        public OrderController(IOrderService orderService, AppDbContext context)
        {
            _orderService = orderService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                // Get orders with calculated totals
                var orders = _context.Orders
                    .Include(o => o.NavCustomer)
                    .Include(o => o.NavTable)
                    .Include(o => o.NavOrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .Where(o => !o.IsDeleted)
                    .AsNoTracking()
                    .ToList();

                // Calculate and update missing values
                var ordersToUpdate = new List<DAL.Models.Order>();

                foreach (var order in orders)
                {
                    // Check if OrderItems exist and calculation is needed
                    if (order.NavOrderItems != null && order.NavOrderItems.Any())
                    {
                        var calculatedSubTotal = (int)order.NavOrderItems.Sum(oi => oi.TotalPrice);
                        var calculatedServiceTax = (int)(calculatedSubTotal * 0.10m);
                        var calculatedTotal = calculatedSubTotal + calculatedServiceTax - order.Discount;

                        // Update if values are different
                        if (order.SubTotal != calculatedSubTotal ||
                            order.ServiceTax != calculatedServiceTax ||
                            order.Total != calculatedTotal)
                        {
                            order.SubTotal = calculatedSubTotal;
                            order.ServiceTax = calculatedServiceTax;
                            order.Total = calculatedTotal;

                            // Track for update
                            var orderToUpdate = _context.Orders.Find(order.Id);
                            if (orderToUpdate != null)
                            {
                                orderToUpdate.SubTotal = calculatedSubTotal;
                                orderToUpdate.ServiceTax = calculatedServiceTax;
                                orderToUpdate.Total = calculatedTotal;
                            }
                        }
                    }
                }

                // Save all updates at once
                _context.SaveChanges();

                // Map to DTOs and order by ID descending (newest first)
                var orderDTOs = orders
                    .OrderByDescending(o => o.Id)
                    .Select(o => new OrderDTO
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId ?? 0,
                        ServiceTax = o.ServiceTax,
                        Total = o.Total,
                        PaymentStatus = o.PaymentStatus ?? "Pending",
                        OrderType = o.OrderType ?? "Unknown",
                        Status = o.Status ?? "Pending",
                        TableId = o.TableId ?? 0,
                        CreatedBy = o.CreatedBy,
                        CreatedOn = o.CreatedOn
                    }).ToList();

                return View(orderDTOs);
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error in Order Index: {ex.Message}");
                return View(new List<OrderDTO>());
            }
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var order = _context.Orders
                .Include(o => o.NavCustomer)
                .Include(o => o.NavTable)
                .Include(o => o.NavOrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefault(o => o.Id == Id && !o.IsDeleted);

            if (order == null) return NotFound();

            // Ensure calculations are up to date
            if (order.SubTotal == 0 && order.NavOrderItems.Any())
            {
                order.SubTotal = (int)order.NavOrderItems.Sum(oi => oi.TotalPrice);
                order.ServiceTax = (int)(order.SubTotal * 0.10m);
                order.Total = order.SubTotal + order.ServiceTax - order.Discount;
                _context.SaveChanges();
            }

            var orderDetails = new OrderDetailsDTO
            {
                Id = order.Id,
                CustomerId = order.CustomerId ?? 0,
                SubTotal = order.SubTotal,
                ServiceTax = order.ServiceTax,
                Total = order.Total,
                PaymentStatus = order.PaymentStatus ?? "Unknown",
                Discount = order.Discount,
                OrderType = order.OrderType ?? "Unknown",
                Status = order.Status ?? "Pending",
                TableId = order.TableId ?? 0,
                CreatedBy = order.CreatedBy,
                CreatedOn = order.CreatedOn
            };

            return View(orderDetails);
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
            if (order == null) return NotFound();

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
            try
            {
                // Check if order exists
                var order = _context.Orders
                    .Include(o => o.NavOrderItems)
                    .Include(o => o.NavPayments)
                    .FirstOrDefault(o => o.Id == Id);

                if (order == null)
                {
                    TempData["Message"] = "Order not found.";
                    TempData["MessageType"] = "error";
                    return RedirectToAction("Index");
                }

                // Check if order is paid (prevent deletion of paid orders)
                if (order.PaymentStatus?.ToLower() == "paid")
                {
                    TempData["Message"] = "Cannot delete a paid order.";
                    TempData["MessageType"] = "error";
                    return RedirectToAction("Index");
                }

                // Soft delete: mark as deleted instead of removing
                order.IsDeleted = true;

                // Also soft delete related order items
                if (order.NavOrderItems != null)
                {
                    foreach (var item in order.NavOrderItems)
                    {
                        item.IsDeleted = true;
                    }
                }

                _context.SaveChanges();

                TempData["Message"] = $"Order #{Id} deleted successfully.";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Error deleting order: {ex.Message}";
                TempData["MessageType"] = "error";
            }

            return RedirectToAction("Index");
        }


    }
}
