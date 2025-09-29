using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.Services.Interfaces;

namespace Restaurant.PL.Controllers
{
    public class CustomerController(ICustomerService _customerService) : Controller
    {
        public IActionResult Index()
        {
            var customers= _customerService.GetAllCustomers();
            return View(customers);
        }
    }
}
