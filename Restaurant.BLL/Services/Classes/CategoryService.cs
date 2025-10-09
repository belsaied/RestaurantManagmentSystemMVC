using Restaurant.BLL.DTOs.CategoryDTOs;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.DAL.Data.Repositories.Classes;
using Restaurant.DAL.Data.Repositories.Interfaces;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services.Classes
{
    public class CategoryService(IUnitOfWork _unitOfWork) : ICategoryService
    {
        // GetAll

        public IEnumerable<GetAllCategoriesDTO> GetAllCategories()
        {
            var Category = _unitOfWork.CategoryRepository.GetAll()
                                .Where(c => !c.IsDeleted);  // Add this filter
            var CategoryDTO = Category.Select(c => new GetAllCategoriesDTO
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                Description = c.Description,
                DisplayOrder = c.DisplayOrder,
                IsActive = c.IsActive,
                CreatedBy = c.CreatedBy,
                CreatedOn = c.CreatedOn,
                ModifiedBy = c.ModifiedBy,
                ModifiedOn = c.ModifiedOn,
                IsDeleted = c.IsDeleted,
            });
            return CategoryDTO;
        }
        // GetById
        public GetByIdCategoryDTO? GetCategoryById(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null || category.IsDeleted) return null;

            return new GetByIdCategoryDTO
            {
                CategoryName = category.CategoryName,
                Description = category.Description,
                Id = category.Id,
                IsActive = category.IsActive,
                DisplayOrder = category.DisplayOrder,
                IsDeleted = category.IsDeleted,
            };
        }
        // Add:
        public int AddCategory(CreateCategoryDTO createCategoryDTO)
        {
            var category = new Category()
            {
                CategoryName = createCategoryDTO.CategoryName,
                Description = createCategoryDTO.Description,
                DisplayOrder = createCategoryDTO.DisplayOrder,
                IsActive = createCategoryDTO.IsActive,
            };

             _unitOfWork.CategoryRepository.Add(category);
            return _unitOfWork.SaveChanges();
        }
        // Update:
        public int UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
             _unitOfWork.CategoryRepository.Update(new Category()
            {
                CategoryName = updateCategoryDTO.CategoryName,
                Description = updateCategoryDTO.Description,
                IsActive = updateCategoryDTO.IsActive,
                Id = updateCategoryDTO.Id,
                DisplayOrder = updateCategoryDTO.DisplayOrder,
            });
            return _unitOfWork.SaveChanges();   
        }

        // 1 Get categories for dropdown lists  (اللي هو لو في Categories خلصت )  -> (دة يجيب اللي موجود مخلصش)
        public List<CategorySelectDTO> GetAllActiveCategories()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll()
                                .Where(c => c.IsActive && !c.IsDeleted) // filter active categories
                                .ToList();
            var result = new List<CategorySelectDTO>();
            foreach (var category in categories)
            {
                result.Add(new CategorySelectDTO
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                    IsActive = category.IsActive
                });
            }

            return result;
        }

        //  Get categories with item count
        public List<CountItemsInCategoryDTO> GetCategoriesWithItemCount()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll()
                                .Where(c => !c.IsDeleted)
                                .ToList();


            var result = new List<CountItemsInCategoryDTO>();
            foreach (var category in categories)
            {
                result.Add(new CountItemsInCategoryDTO
                {
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    DisplayOrder = category.DisplayOrder,
                    IsActive = category.IsActive,

                    // Additional properties from CategoryDto
                    Id = category.Id,


                    // Additional property from CountItemsInCategoryDTO
                    MenuItemsCount = category.MenuItems?.Count(mi => !mi.IsDeleted) ?? 0
                });
            }

            return result;
        }

        // Delete
        public bool DeleteCategory(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
            {
                return false;
            }
            _unitOfWork.CategoryRepository.DeleteById(category.Id);
            int numberOfRows = _unitOfWork.SaveChanges();
            return numberOfRows > 0 ? true : false;
        }
    }
}
