using AutoMapper;
using Restaurant.BLL.DTOs.RecipeLinesDtos;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.DAL.Data.Repositories.Interfaces;
using Restaurant.DAL.Models;


namespace Restaurant.BLL.Services.Classes
{
    public class RecipeLineServices : IRecipeLineServices
    {
        #region Old.

        //public int AddRecipe(CreateRecipeDto ingredientDto)
        //{
        //    _unitOfWork.RecipeLineRepository.Add(_mapper.Map<CreateRecipeDto, RecipeLine>(ingredientDto));
        //    return _unitOfWork.SaveChanges();
        //}

        //public IEnumerable<RecipeDto> GetAllRecipes(bool withTracking = false)
        //{
        //    var recipes = _unitOfWork.RecipeLineRepository.GetAll(withTracking);

        //    return _mapper.Map<IEnumerable<RecipeLine>, IEnumerable<RecipeDto>>(recipes);
        //}

        //public RecipesDetailsDto? GetRecipeById(int id)
        //{
        //    var recipe = _unitOfWork.RecipeLineRepository.GetById(id);
        //    if (recipe == null) return null;
        //    return _mapper.Map<RecipeLine?, RecipesDetailsDto?>(recipe);
        //}

        //public bool RemoveRecipe(int id)
        //{
        //    var recipe = _unitOfWork.RecipeLineRepository.GetById(id);
        //    if (recipe == null) return false;
        //    recipe.IsDeleted = true;
        //    _unitOfWork.RecipeLineRepository.Update(recipe);
        //    return _unitOfWork.SaveChanges() > 0 ? true : false;
        //}

        //public int UpdateRecipe(UpdatedRecipeDto ingredientDto)
        //{
        //    var recipe = _mapper.Map<UpdatedRecipeDto, RecipeLine>(ingredientDto);
        //    _unitOfWork.RecipeLineRepository.Update(recipe);
        //    return _unitOfWork.SaveChanges();
        //}  

        #endregion

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecipeLineServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<RecipeLineDto> GetAllRecipeLines(bool withTracking = false)
        {
            var recipeLines = _unitOfWork.RecipeLineRepository.GetAll(withTracking);
            return _mapper.Map<IEnumerable<RecipeLine>, IEnumerable<RecipeLineDto>>(recipeLines);
        }

        public RecipeLineDetailsDto? GetRecipeLineById(int id)
        {
            var recipeLine = _unitOfWork.RecipeLineRepository.GetById(id);
            return recipeLine == null ? null : _mapper.Map<RecipeLine, RecipeLineDetailsDto>(recipeLine);
        }

        public List<RecipeLineSelectDto> GetRecipeLinesByMenuItem(int menuItemId)
        {
            var recipeLines = _unitOfWork.RecipeLineRepository.GetAll()
                .Where(rl => rl.MenuId == menuItemId && !rl.IsDeleted)
                .ToList();

            return _mapper.Map<List<RecipeLine>, List<RecipeLineSelectDto>>(recipeLines);
        }

        public List<RecipeLineSelectDto> GetRecipeLinesByIngredient(int ingredientId)
        {
            var recipeLines = _unitOfWork.RecipeLineRepository.GetAll()
                .Where(rl => rl.IngredientId == ingredientId && !rl.IsDeleted)
                .ToList();

            return _mapper.Map<List<RecipeLine>, List<RecipeLineSelectDto>>(recipeLines);
        }

        public int AddRecipeLine(CreateRecipeLineDto recipeLineDto)
        {
            try
            {
                // Validate that the menu item and ingredient exist
                var menuItem = _unitOfWork.MenuItemRepository.GetById(recipeLineDto.MenuId);
                if (menuItem == null || menuItem.IsDeleted)
                    throw new InvalidOperationException("Menu item does not exist or is deleted.");

                var ingredient = _unitOfWork.IngredientRepository.GetById(recipeLineDto.IngredientId);
                if (ingredient == null || ingredient.IsDeleted)
                    throw new InvalidOperationException("Ingredient does not exist or is deleted.");

                var recipeLine = _mapper.Map<CreateRecipeLineDto, RecipeLine>(recipeLineDto);
                _unitOfWork.RecipeLineRepository.Add(recipeLine);
                return _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateRecipeLine(UpdateRecipeLineDto recipeLineDto)
        {
            try
            {
                // Fetch the existing tracked entity
                var existingRecipeLine = _unitOfWork.RecipeLineRepository.GetById(recipeLineDto.Id);

                if (existingRecipeLine == null || existingRecipeLine.IsDeleted)
                    return 0;

                // Validate that the menu item and ingredient exist
                var menuItem = _unitOfWork.MenuItemRepository.GetById(recipeLineDto.MenuId);
                if (menuItem == null || menuItem.IsDeleted)
                    throw new InvalidOperationException("Menu item does not exist or is deleted.");

                var ingredient = _unitOfWork.IngredientRepository.GetById(recipeLineDto.IngredientId);
                if (ingredient == null || ingredient.IsDeleted)
                    throw new InvalidOperationException("Ingredient does not exist or is deleted.");

                // Update properties
                existingRecipeLine.Quantity = Convert.ToInt32(recipeLineDto.Quantity);
                existingRecipeLine.Unit = recipeLineDto.Unit;
                existingRecipeLine.IngredientId = recipeLineDto.IngredientId;
                existingRecipeLine.MenuId = recipeLineDto.MenuId;
                existingRecipeLine.ModifiedOn = DateTime.Now;

                return _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteRecipeLine(int id)
        {
            var recipeLine = _unitOfWork.RecipeLineRepository.GetById(id);
            if (recipeLine == null)
                return false;

            recipeLine.IsDeleted = true;
            _unitOfWork.RecipeLineRepository.Update(recipeLine);
            return _unitOfWork.SaveChanges() > 0;
        }
    }
}
