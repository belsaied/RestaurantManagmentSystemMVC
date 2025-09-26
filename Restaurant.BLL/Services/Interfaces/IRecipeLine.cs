using Restaurant.BLL.DTOs.IngredientModule;
using Restaurant.BLL.DTOs.RecipeLinesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services.Interfaces
{
    internal interface IRecipeLineServices
    {
        public IEnumerable<RecipeDto> GetAllRecipes(bool withTracking = false);

        //Get ingredient by Id

        public RecipeDto? GetRecipeById(int id);

        //Add Ingredient

        public int AddRecipe(CreateRecipeDto ingredientDto);

        //Remove Ingredient
        public bool RemoveRecipe(int id);
        //Update Ingredient
        public int UpdateRecipe(RecipeDto ingredientDto);
    }
}
