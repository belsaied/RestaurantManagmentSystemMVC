using AutoMapper;
using Restaurant.BLL.DTOs.RecipeLinesDtos;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.DAL.Data.Repositories.Classes;
using Restaurant.DAL.Data.Repositories.Interfaces;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Restaurant.BLL.Services.Classes
{
    internal class RecipeLineServices(IRecipeLineRepository _Recipes,IMapper _mapper) : IRecipeLineServices
    {
        public int AddRecipe(CreateRecipeDto ingredientDto)
        {
            return _Recipes.AddRecipeLine(_mapper.Map<CreateRecipeDto, RecipeLine>(ingredientDto));
        }

        public IEnumerable<RecipeDto> GetAllRecipes(bool withTracking = false)
        {
            var recipes = _Recipes.GetAllRecipeLines(withTracking);
       
            return _mapper.Map<IEnumerable<RecipeLine>, IEnumerable<RecipeDto>>(recipes);
        }

        public RecipeDto? GetRecipeById(int id)
        {
           var recipe=_Recipes.GetRecipeLineById(id);
            if (recipe == null) return null;
            return _mapper.Map<RecipeLine?, RecipeDto?>(recipe);
        }

        public bool RemoveRecipe(int id)
        {
            var recipe = _Recipes.GetRecipeLineById(id);
            if(recipe == null) return false;
            recipe.IsDeleted = true;
            return _Recipes.UpdateRecipeLine(recipe) > 0;
        }

        public int UpdateRecipe(RecipeDto ingredientDto)
        {
          var recipe=  _mapper.Map<RecipeDto, RecipeLine>(ingredientDto);
            return _Recipes.UpdateRecipeLine(recipe);
        }
    }
}
