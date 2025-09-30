using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.DTOs.RecipeLinesDtos;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.PL.ViewModels.RecipeLineVM;

namespace Restaurant.PL.Controllers
{
    public class RecipeLineController(IRecipeLineServices _recipeLineServices) : Controller
    {
        #region Index
        public IActionResult Index()
        {
            var recipes = _recipeLineServices.GetAllRecipes();

            return View(recipes);
        }
        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RecipeLineViewModel recipeLine)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _recipeLineServices.AddRecipe(new CreateRecipeDto
                    {
                        Quantity = recipeLine.Quantity,
                        Unit = recipeLine.Unit,
                        IngredientId = recipeLine.IngredientId,
                        MenuId = recipeLine.MenuId
                    });
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid data.");
            }
            return View(recipeLine);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var recipe = _recipeLineServices.GetRecipeById(id.Value);
            if (recipe == null)
            {
                return NotFound();
            }
            var recipeLineViewModel = new RecipeLineViewModel
            {
                Quantity = recipe.Quantity,
                Unit = recipe.Unit,
                IngredientId= recipe.IngredientId,
                MenuId= recipe.MenuId
            };
            return View(recipeLineViewModel);
        }
        [HttpPost]
        public IActionResult Edit(int? id, RecipeLineViewModel recipeLine)
        {
            if (!id.HasValue ||id == 0)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _recipeLineServices.UpdateRecipe( new RecipeDto
                    {
                        Id = id.Value,
                        Quantity = recipeLine.Quantity,
                        Unit = recipeLine.Unit,
                        IngredientId = recipeLine.IngredientId,
                        MenuId = recipeLine.MenuId
                    });
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid data.");
            }
            return View(recipeLine);
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            try
            {
             bool isDeleted=   _recipeLineServices.RemoveRecipe(id.Value);
                if (isDeleted)
                {

                return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Recipe Can not be Deleted");

                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, "An error occurred while processing your request.");
            }
            return RedirectToAction(nameof(Delete),new {id});
        }
        #endregion
    }
}
