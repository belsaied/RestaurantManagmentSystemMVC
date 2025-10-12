using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.DTOs.IngredientModule;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.PL.ViewModels.IngredientVM;

namespace Restaurant.PL.Controllers
{
    [Authorize]

    public class IngredientController(IIngredientServices _ingredients) : Controller
    {
        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var ingredients = _ingredients.GetAllIngredients();

            return View(ingredients);
        }
        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IngredientViewModel ingredientVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _ingredients.AddIngredient(new CreateIngredientDto
                    {
                        Name = ingredientVM.Name,
                        Unit = ingredientVM.Unit,
                        UnitCost = ingredientVM.UnitCost,
                        CurrentStock = ingredientVM.CurrentStock,
                        MinStock = ingredientVM.MinStock,
                        IsActive = ingredientVM.IsActive
                    });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            else {
                ModelState.AddModelError(string.Empty, "Ingredient Can not be Created");
            }
            return View(ingredientVM);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var ingredient = _ingredients.GetIngredientById(id.Value);
            if (ingredient == null)
                return NotFound();
            var ingredientVM = new IngredientViewModel
            {
                Name = ingredient.Name,
                Unit = ingredient.Unit,
                UnitCost = ingredient.UnitCost,
                CurrentStock = ingredient.CurrentStock,
                MinStock = ingredient.MinStock,
                IsActive = ingredient.IsActive
            };
            return View(ingredientVM);
        }

        [HttpPost]
        public IActionResult Edit(int? id, IngredientViewModel ingredientVM)
        {
            if (!id.HasValue)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var ingredient = new IngredientDto
                    {
                        Id = id.Value,
                        Name = ingredientVM.Name,
                        Unit = ingredientVM.Unit,
                        UnitCost = ingredientVM.UnitCost,
                        CurrentStock = ingredientVM.CurrentStock,
                        MinStock = ingredientVM.MinStock,
                        IsActive = ingredientVM.IsActive,
                       

                    };
                    _ingredients.UpdateIngredient(ingredient);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ingredient Can not be Updated");
            }
            return View(ingredientVM);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id) { 
        
            if(!id.HasValue) return BadRequest();
            var Ingred=_ingredients.GetIngredientById(id.Value);
            if(Ingred == null) return NotFound();
            
            return View(Ingred);
        
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue || id == 0)
                return BadRequest();
            try
            {

                bool isDeleted = _ingredients.RemoveIngredient(id.Value);
                if (isDeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Ingredient Can not be Deleted");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
            #endregion


        }
}
