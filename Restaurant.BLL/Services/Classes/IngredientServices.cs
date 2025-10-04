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
    public class IngredientServices(IIngredientRepository _ingredients, IMapper _mapper) : IIngredientServices
    {
        public int AddIngredient(CreateIngredientDto ingredientDto)
        {
            var ingredient = _mapper.Map<CreateIngredientDto,Ingredient>(ingredientDto);
            return _ingredients.AddIngredient(ingredient);
        }

        public IEnumerable<IngredientDto> GetAllIngredients(bool withTracking = false)
        {
            var ingredients= _ingredients.GetAllIngredient(withTracking).Where(I=>I.IsDeleted==false);   

            return _mapper.Map<IEnumerable<Ingredient>, IEnumerable<IngredientDto>>(ingredients);
        }

        public IngredientDetailsDto? GetIngredientById(int id)
        {
            var ingredient= _ingredients.GetIngredientById(id);
            return _mapper.Map<Ingredient?, IngredientDetailsDto?>(ingredient);
        }

        public bool RemoveIngredient(int id)
        {
            var ingredient= _ingredients.GetIngredientById(id);
            if (ingredient == null) return false;
            ingredient.IsDeleted = true;
           

            return _ingredients.UpdateIngredient(ingredient)>0;

        }

        public int UpdateIngredient(IngredientDto ingredientDto)
        {
           return _ingredients.UpdateIngredient(_mapper.Map<IngredientDto, Ingredient>(ingredientDto));
        }

       
    }
}
