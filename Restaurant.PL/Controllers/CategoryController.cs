using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.DTOs.CategoryDTOs;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.PL.ViewModels.CategoryVM;

namespace Restaurant.PL.Controllers
{
    public class CategoryController(ICategoryService _categoryService, IHostEnvironment _env, ILogger<CategoryController> _logger) : Controller
    {
        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }
        #endregion

        #region Create (Get/Post)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _categoryService.AddCategory(new CreateCategoryDTO()
                    {
                        CategoryName = categoryViewModel.CategoryName,
                        Description = categoryViewModel.Description,
                        DisplayOrder = categoryViewModel.DisplayOrder,
                        IsActive = categoryViewModel.IsActive
                    });
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Category can't be created");
                    }
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                    {
                        _logger.LogError($"Category can't be created {ex.Message}");
                    }
                    else
                    {
                        _logger.LogError($"Category can't be created because :{ex}");
                        return View("ErrorView", ex);
                    }
                }
            }

            return View(categoryViewModel);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var category = _categoryService.GetCategoryById(id.Value);
            return (category is null) ? NotFound() : View(category);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var category = _categoryService.GetCategoryById(id.Value);
            if (category is null) return NotFound();

            var categoryVM = new CategoryViewModel()
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                DisplayOrder = category.DisplayOrder,
                IsActive = category.IsActive
            };

            return View(categoryVM);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, CategoryViewModel categoryViewModel)
        {
            if (!id.HasValue) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }

            try
            {
                int result = _categoryService.UpdateCategory(new UpdateCategoryDTO()
                {
                    Id = id.Value,
                    CategoryName = categoryViewModel.CategoryName,
                    Description = categoryViewModel.Description,
                    DisplayOrder = categoryViewModel.DisplayOrder,
                    IsActive = categoryViewModel.IsActive
                });
                if (result > 0) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Category Can't be updated");
                    return View(categoryViewModel);
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    _logger.LogError($"Category can't be Updated {ex.Message}");
                }
                else
                {
                    _logger.LogError($"Category can't be updated because {ex}");
                    return View("ErrorView", ex);
                }
            }
            return View(categoryViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool isDeleted = _categoryService.DeleteCategory(id);
                if (isDeleted)
                {
                    TempData["Message"] = "Category deleted successfully";
                    TempData["MessageType"] = "success";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Category Can't be Deleted";
                    TempData["MessageType"] = "error";
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    _logger.LogError($"Category can't be Deleted {ex.Message}");
                }
                else
                {
                    _logger.LogError($"Category Can't be Deleted because {ex}");
                }
                TempData["Message"] = "An error occurred while deleting the category";
                TempData["MessageType"] = "error";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}