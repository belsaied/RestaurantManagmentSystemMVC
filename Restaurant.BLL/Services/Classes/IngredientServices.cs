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
    public class IngredientServices(DAL.Data.Repositories.Interfaces.IIngredientRepository _ingredients, IMapper _mapper) : IIngredientServices
    {
        public int AddIngredient(CreateIngredientDto ingredientDto)
        {
            var ingredient = _mapper.Map<CreateIngredientDto,Ingredient>(ingredientDto);
            return _ingredients.Add(ingredient);
        }

        public IEnumerable<IngredientDto> GetAllIngredients(bool withTracking = false)
        {
            var ingredients= _ingredients.GetAll(withTracking).Where(I=>I.IsDeleted==false);   

            return _mapper.Map<IEnumerable<Ingredient>, IEnumerable<IngredientDto>>(ingredients);
        }

        public IngredientDetailsDto? GetIngredientById(int id)
        {
            var ingredient= _ingredients.GetById(id);
            return _mapper.Map<Ingredient?, IngredientDetailsDto?>(ingredient);
        }

        public bool RemoveIngredient(int id)
        {
            var ingredient= _ingredients.GetById(id);
            if (ingredient == null) return false;
            ingredient.IsDeleted = true;
           

            return _ingredients.Update(ingredient)>0;

        }

        public int UpdateIngredient(IngredientDto ingredientDto)
        {
           return _ingredients.Update(_mapper.Map<IngredientDto, Ingredient>(ingredientDto));
        }

       
    }
}
