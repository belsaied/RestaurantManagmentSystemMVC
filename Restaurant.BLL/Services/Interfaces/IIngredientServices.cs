using Restaurant.BLL.DTOs.IngredientModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services.Interfaces
{
    internal interface IIngredientServices
    {
        //Get all ingredients
        public IEnumerable<IngredientDto> GetAllIngredients(bool withTracking = false);

        //Get ingredient by Id

        public IngredientDto? GetIngredientById(int id);

        //Add Ingredient

       public int AddIngredient(IngredientDto ingredientDto);

        //Remove Ingredient
        public bool RemoveIngredient(int id);
        //Update Ingredient
        public int UpdateIngredient(IngredientDto ingredientDto);

    }
}
