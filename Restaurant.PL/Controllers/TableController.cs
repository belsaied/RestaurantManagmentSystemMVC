using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.DTOs.TableModule;
using Restaurant.BLL.Services.Interfaces;

namespace Restaurant.PL.Controllers
{
    public class TableController(ITableService _tableService) : Controller
    {
        public IActionResult Index()
        {
            return View(_tableService.GetAll());
        }
        public IActionResult Details(int Id)
        {
            return View(_tableService.GetById(Id));
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CreatedTableDto dtoFromView)
        {
            if (ModelState.IsValid)
            {
                _tableService.Add(dtoFromView);
                return RedirectToAction("Index");
            }
            return View(dtoFromView);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            _tableService.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
