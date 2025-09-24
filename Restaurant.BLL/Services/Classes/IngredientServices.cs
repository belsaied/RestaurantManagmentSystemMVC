using AutoMapper;
using Restaurant.BLL.DTOs.IngredientModule;
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
    internal class IngredientServices(IIngredientRepository _ingredients, IMapper _mapper) : IIngredientServices
    {
        public int AddIngredient(IngredientDto ingredientDto)
        {
            var ingredient = _mapper.Map<IngredientDto,Ingredient>(ingredientDto);
            return _ingredients.AddIngredient(ingredient);
        }

        public IEnumerable<IngredientDto> GetAllIngredients(bool withTracking = false)
        {
            var ingredients= _ingredients.GetAllIngredient(withTracking);   

            return _mapper.Map<IEnumerable<Ingredient>, IEnumerable<IngredientDto>>(ingredients);
        }

        public IngredientDto? GetIngredientById(int id)
        {
            var ingredient= _ingredients.GetIngredientById(id);
            return _mapper.Map<Ingredient?, IngredientDto?>(ingredient);
        }

        public bool RemoveIngredient(int id)
        {
            var ingredient= _ingredients.GetIngredientById(id);
            if (ingredient == null) return false;
            ingredient.IsActive = false;
            return _ingredients.UpdateIngredient(ingredient)>0;

        }

        public int UpdateIngredient(IngredientDto ingredientDto)
        {
           return _ingredients.UpdateIngredient(_mapper.Map<IngredientDto, Ingredient>(ingredientDto));
        }

       
    }
}
