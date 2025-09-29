using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.DTOs.CustomerDTOs;
using Restaurant.BLL.Services.Interfaces;

namespace Restaurant.PL.Controllers
{
    public class CustomerController(ICustomerService _customerService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var customers= _customerService.GetAllCustomers();
            return View(customers);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CreateCustomerDTO customerDTO)
        {
            if(ModelState.IsValid)
            {
                _customerService.AddCustomer(customerDTO);
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var customer=_customerService.GetCustomerById(Id);
            return View(customer);
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdateCustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                _customerService.UpdateCustomer(customerDTO);
                return RedirectToAction("Index");
            }
           return View(customerDTO);


        }

        public IActionResult Delete(int Id)
        {
            _customerService.DeleteCustomer(Id);
            return RedirectToAction(nameof(Index));
        }






    }
}
