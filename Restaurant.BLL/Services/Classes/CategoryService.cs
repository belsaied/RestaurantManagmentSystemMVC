using Restaurant.BLL.AttachmentService;
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
    public class CategoryService(IUnitOfWork _unitOfWork, IAttachmentService _attachmentService) : ICategoryService
    {
        // GetAll
        public IEnumerable<GetAllCategoriesDTO> GetAllCategories()
        {
            var Category = _unitOfWork.CategoryRepository.GetAll()
                                .Where(c => !c.IsDeleted);
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
                ImageName = c.ImageName
            });
            return CategoryDTO;
        }

        // GetById - No changes needed here
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
                ImageName = category.ImageName
            };
        }

        // Add - No changes needed here
        public int AddCategory(CreateCategoryDTO createCategoryDTO)
        {
            try
            {
                var category = new Category()
                {
                    CategoryName = createCategoryDTO.CategoryName,
                    Description = createCategoryDTO.Description,
                    DisplayOrder = createCategoryDTO.DisplayOrder,
                    IsActive = createCategoryDTO.IsActive,
                };

                // Handle image upload
                if (createCategoryDTO.Image is not null)
                {
                    string? imgName = _attachmentService.Upload(createCategoryDTO.Image, "Categories");
                    if (imgName is not null)
                    {
                        category.ImageName = imgName;
                    }
                }

                _unitOfWork.CategoryRepository.Add(category);
                return _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Update - FIXED VERSION
        public int UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            try
            {
                // Fetch the existing tracked entity from the database
                var existingCategory = _unitOfWork.CategoryRepository.GetById(updateCategoryDTO.Id);

                if (existingCategory == null || existingCategory.IsDeleted)
                {
                    return 0; // Category not found or deleted
                }

                // Update the properties of the EXISTING tracked entity
                existingCategory.CategoryName = updateCategoryDTO.CategoryName;
                existingCategory.Description = updateCategoryDTO.Description;
                existingCategory.IsActive = updateCategoryDTO.IsActive;
                existingCategory.DisplayOrder = updateCategoryDTO.DisplayOrder;
                existingCategory.ModifiedOn = DateTime.Now;
                // existingCategory.ModifiedBy = "CurrentUser"; // Set this if you have user context

                // Handle new image upload
                if (updateCategoryDTO.Image is not null)
                {
                    string? imgName = _attachmentService.Upload(updateCategoryDTO.Image, "Categories");
                    if (imgName is not null)
                    {
                        existingCategory.ImageName = imgName;
                    }
                }
                else if (!string.IsNullOrEmpty(updateCategoryDTO.ImageName))
                {
                    // Keep the existing image if no new image uploaded
                    existingCategory.ImageName = updateCategoryDTO.ImageName;
                }

                // No need to call Update() - EF Core is already tracking the entity
                // Just save the changes
                return _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CategorySelectDTO> GetAllActiveCategories()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll()
                                .Where(c => c.IsActive && !c.IsDeleted)
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
                    Id = category.Id,
                    MenuItemsCount = category.MenuItems?.Count(mi => !mi.IsDeleted) ?? 0
                });
            }

            return result;
        }

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