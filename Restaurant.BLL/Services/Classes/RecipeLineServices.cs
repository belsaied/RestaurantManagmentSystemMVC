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
    public class RecipeLineServices(IUnitOfWork _unitOfWork,IMapper _mapper) : IRecipeLineServices
    {
        public int AddRecipe(CreateRecipeDto ingredientDto)
        {
             _unitOfWork.RecipeLineRepository.Add(_mapper.Map<CreateRecipeDto, RecipeLine>(ingredientDto));
        return _unitOfWork.SaveChanges();
        }

        public IEnumerable<RecipeDto> GetAllRecipes(bool withTracking = false)
        {
            var recipes = _unitOfWork.RecipeLineRepository.GetAll(withTracking);
       
            return _mapper.Map<IEnumerable<RecipeLine>, IEnumerable<RecipeDto>>(recipes);
        }

        public RecipesDetailsDto? GetRecipeById(int id)
        {
           var recipe=_unitOfWork.RecipeLineRepository.GetById(id);
            if (recipe == null) return null;
            return _mapper.Map<RecipeLine?, RecipesDetailsDto?>(recipe);
        }

        public bool RemoveRecipe(int id)
        {
            var recipe = _unitOfWork.RecipeLineRepository.GetById(id);
            if(recipe == null) return false;
            recipe.IsDeleted = true;
             _unitOfWork.RecipeLineRepository.Update(recipe) ;
            return _unitOfWork.SaveChanges() > 0 ? true : false;
        }

        public int UpdateRecipe(UpdatedRecipeDto ingredientDto)
        {
          var recipe=  _mapper.Map<UpdatedRecipeDto, RecipeLine>(ingredientDto);
             _unitOfWork.RecipeLineRepository.Update(recipe);
            return _unitOfWork.SaveChanges();
        }
    }
}
