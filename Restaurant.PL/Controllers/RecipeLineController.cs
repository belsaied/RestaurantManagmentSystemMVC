using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.DTOs.RecipeLinesDtos;
using Restaurant.BLL.Services.Classes;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.PL.ViewModels.RecipeLineVM;

namespace Restaurant.PL.Controllers
{
    [Authorize]

    public class RecipeLineController : Controller
    {
        private readonly IRecipeLineServices _recipeLineService;
        private readonly IIngredientServices _ingredientService;
        private readonly IMenuItemServices _menuItemService;
        private readonly ILogger<RecipeLineController> _logger;
        private readonly IHostEnvironment _env;

        public RecipeLineController(
            IRecipeLineServices recipeLineService,
            IIngredientServices ingredientService,
            IMenuItemServices menuItemService,
            ILogger<RecipeLineController> logger,
            IHostEnvironment env)
        {
            _recipeLineService = recipeLineService;
            _ingredientService = ingredientService;
            _menuItemService = menuItemService;
            _logger = logger;
            _env = env;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var recipeLines = _recipeLineService.GetAllRecipeLines();

            // Calculate statistics
            var recipeList = recipeLines.ToList();
            ViewBag.TotalLines = recipeList.Count;
            ViewBag.UniqueMenuItems = recipeList.Select(r => r.MenuItemName).Distinct().Count();
            ViewBag.UniqueIngredients = recipeList.Select(r => r.IngredientName).Distinct().Count();
            ViewBag.AvgPerItem = ViewBag.UniqueMenuItems > 0
                ? Math.Round((double)ViewBag.TotalLines / ViewBag.UniqueMenuItems, 1)
                : 0;

            return View(recipeLines);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RecipeLineViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createDto = new CreateRecipeLineDto
                    {
                        Quantity = viewModel.Quantity,
                        Unit = viewModel.Unit,
                        IngredientId = viewModel.IngredientId,
                        MenuId = viewModel.MenuId
                    };

                    int result = _recipeLineService.AddRecipeLine(createDto);

                    if (result > 0)
                    {
                        TempData["Message"] = "Recipe Line created successfully";
                        TempData["MessageType"] = "success";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Recipe Line cannot be created");
                    }
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                        _logger.LogError($"Recipe Line cannot be created: {ex.Message}");
                    else
                        _logger.LogError($"Recipe Line cannot be created: {ex}");

                    ModelState.AddModelError(string.Empty, "An error occurred while creating the recipe line");
                }
            }

            PopulateDropdowns();
            return View(viewModel);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var recipeLine = _recipeLineService.GetRecipeLineById(id.Value);
            if (recipeLine == null)
            {
                TempData["Message"] = "Recipe Line not found";
                TempData["MessageType"] = "error";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new RecipeLineViewModel
            {
                Id = recipeLine.Id,
                Quantity = recipeLine.Quantity,
                Unit = recipeLine.Unit,
                IngredientId = recipeLine.IngredientId,
                MenuId = recipeLine.MenuId,
                IngredientName = recipeLine.IngredientName,
                MenuItemName = recipeLine.MenuItemName
            };

            PopulateDropdowns();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, RecipeLineViewModel viewModel)
        {
            if (!id.HasValue || id.Value != viewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(viewModel);
            }

            try
            {
                var updateDto = new UpdateRecipeLineDto
                {
                    Id = viewModel.Id,
                    Quantity = viewModel.Quantity,
                    Unit = viewModel.Unit,
                    IngredientId = viewModel.IngredientId,
                    MenuId = viewModel.MenuId
                };

                int result = _recipeLineService.UpdateRecipeLine(updateDto);

                if (result > 0)
                {
                    TempData["Message"] = "Recipe Line updated successfully";
                    TempData["MessageType"] = "success";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Recipe Line cannot be updated");
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    _logger.LogError($"Recipe Line cannot be updated: {ex.Message}");
                else
                    _logger.LogError($"Recipe Line cannot be updated: {ex}");

                ModelState.AddModelError(string.Empty, "An error occurred while updating the recipe line");
            }

            PopulateDropdowns();
            return View(viewModel);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var recipeLine = _recipeLineService.GetRecipeLineById(id.Value);
            if (recipeLine == null)
                return NotFound();

            return View(recipeLine);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            try
            {
                bool isDeleted = _recipeLineService.DeleteRecipeLine(id);
                if (isDeleted)
                {
                    TempData["Message"] = "Recipe Line deleted successfully";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Recipe Line cannot be deleted";
                    TempData["MessageType"] = "error";
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    _logger.LogError($"Recipe Line cannot be deleted: {ex.Message}");
                else
                    _logger.LogError($"Recipe Line cannot be deleted: {ex}");

                TempData["Message"] = "An error occurred while deleting the recipe line";
                TempData["MessageType"] = "error";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helper Methods
        private void PopulateDropdowns()
        {
            ViewBag.Ingredients = _ingredientService.GetAllIngredients()
                .Where(i => i.IsActive)
                .Select(i => new { i.Id, i.Name })
                .ToList();

            var allMenuItems = _menuItemService.GetAllMenuItems();
            ViewBag.MenuItems = allMenuItems
                .Where(m => m.IsAvailable && !m.IsDeleted)
                .Select(m => new { m.Id, m.ItemName })
                .ToList();
        }
        #endregion
    }
}
