using Restaurant.BLL.DTOs.CategoryDTOs;

namespace Restaurant.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        int AddCategory(CreateCategoryDTO createCategoryDTO);
        bool DeleteCategory(int id);
        List<CategorySelectDTO> GetAllActiveCategories();
        IEnumerable<GetAllCategoriesDTO> GetAllCategories();
        List<CountItemsInCategoryDTO> GetCategoriesWithItemCount();
        GetByIdCategoryDTO? GetCategoryById(int id);
        int UpdateCategory(UpdateCategoryDTO updateCategoryDTO);
    }
}