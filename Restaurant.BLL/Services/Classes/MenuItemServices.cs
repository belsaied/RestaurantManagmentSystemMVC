using Restaurant.BLL.AttachmentService;
using Restaurant.BLL.DTOs.MenuItemDTOs;
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
    public class MenuItemServices(IUnitOfWork _unitOfWork, ICategoryRepository _categoryRepository , IAttachmentService _attachmentService) : IMenuItemServices
    {

        #region Old MenuItemService without Attachement Service .
        // GetAll
        //public IEnumerable<GetAllMenuItemsDto> GetAllMenuItems()
        //{
        //    var menuItems = _unitOfWork.MenuItemRepository.GetAll();

        //    var menuItemDTO = menuItems.Select(m => new GetAllMenuItemsDto
        //    {
        //        Id = m.Id,
        //        ItemName = m.ItemName,
        //        Description = m.Description,
        //        Price = m.Price,
        //        ImageUrl = m.ImageUrl,
        //        IsAvailable = m.IsAvailable,
        //        CategoryId = m.CategoryId,
        //        CreatedBy = m.CreatedBy,
        //        CreatedOn = m.CreatedOn,
        //        ModifiedBy = m.ModifiedBy,
        //        ModifiedOn = m.ModifiedOn,
        //        IsDeleted = m.IsDeleted,
        //    });
        //    return menuItemDTO;
        //}
        //// GetById
        //public GetByIdMenuItemsDto? GetMenuItemById(int id)
        //{
        //    var menuItem = _unitOfWork.MenuItemRepository.GetById(id);
        //    if (menuItem == null || menuItem.IsDeleted) return null;

        //    return new GetByIdMenuItemsDto
        //    {
        //        Id = menuItem.Id,
        //        ItemName = menuItem.ItemName,
        //        Price = menuItem.Price,
        //        IsAvailable = menuItem.IsAvailable,
        //        ImageUrl = menuItem.ImageUrl,

        //    };
        //}

        //// Add
        //public int AddMenuItem(CreateMenuItemDto createMenuItemDTO)
        //{
        //    // Validate category exists and is active
        //    var category = _categoryRepository.GetById(createMenuItemDTO.CategoryId);
        //    if (category == null || category.IsDeleted || !category.IsActive)
        //    {
        //        throw new InvalidOperationException("Category does not exist or is inactive.");
        //    }

        //    var menuItem = new MenuItem()
        //    {
        //        ItemName = createMenuItemDTO.ItemName,
        //        Description = createMenuItemDTO.Description,
        //        Price = createMenuItemDTO.Price,
        //        ImageUrl = createMenuItemDTO.ImageUrl,
        //        IsAvailable = createMenuItemDTO.IsAvailable,
        //        CategoryId = createMenuItemDTO.CategoryId,
        //    };

        //    _unitOfWork.MenuItemRepository.Add(menuItem);
        //    return _unitOfWork.SaveChanges();
        //}

        //// Update
        //public int UpdateMenuItem(UpdateMenuItemDto updateMenuItemDTO)
        //{
        //    // Validate category exists and is active if category is being changed
        //    var category = _categoryRepository.GetById(updateMenuItemDTO.CategoryId);
        //    if (category == null || category.IsDeleted || !category.IsActive)
        //    {
        //        throw new InvalidOperationException("Category does not exist or is inactive.");
        //    }

        //    _unitOfWork.MenuItemRepository.Update(new MenuItem()
        //    {
        //        Id = updateMenuItemDTO.Id,
        //        ItemName = updateMenuItemDTO.ItemName,
        //        Description = updateMenuItemDTO.Description,
        //        Price = updateMenuItemDTO.Price,
        //        ImageUrl = updateMenuItemDTO.ImageUrl,
        //        IsAvailable = updateMenuItemDTO.IsAvailable,
        //        CategoryId = updateMenuItemDTO.CategoryId,
        //    });
        //    return _unitOfWork.SaveChanges();
        //}

        //// Get available menu items for display (اللي هو لو في MenuItems خلصت) -> (دة يجيب اللي موجود مخلصش)
        //public List<MenuItemSelectDto> GetAllAvailableMenuItems()
        //{
        //    var menuItems = _unitOfWork.MenuItemRepository.GetAll()
        //                        .Where(m => m.IsAvailable && !m.IsDeleted)
        //                        .ToList();

        //    var result = new List<MenuItemSelectDto>();
        //    foreach (var menuItem in menuItems)
        //    {
        //        result.Add(new MenuItemSelectDto
        //        {
        //            Id = menuItem.Id,
        //            ItemName = menuItem.ItemName,
        //            Price = menuItem.Price,
        //            IsAvailable = menuItem.IsAvailable,
        //            CategoryName = menuItem.Category?.CategoryName ?? "Unknown"
        //        });
        //    }

        //    return result;
        //}

        //// Get MenuItems By Category
        //public List<MenuItemsByCategoryDto> GetMenuItemsByCategory(int categoryId)
        //{
        //    var menuItems = _unitOfWork.MenuItemRepository.GetAll()
        //                        .Where(m => m.CategoryId == categoryId && !m.IsDeleted)
        //                        .ToList();

        //    var result = new List<MenuItemsByCategoryDto>();
        //    foreach (var menuItem in menuItems)
        //    {
        //        result.Add(new MenuItemsByCategoryDto
        //        {
        //            Id = menuItem.Id,
        //            ItemName = menuItem.ItemName,
        //            Description = menuItem.Description,
        //            price = menuItem.Price,
        //            ImageUrl = menuItem.ImageUrl,
        //            IsAvailable = menuItem.IsAvailable,
        //            CategoryId = menuItem.CategoryId,
        //            CategoryName = menuItem.Category?.CategoryName ?? "Unknown"
        //        });
        //    }

        //    return result;
        //}

        //// Search menu items
        //public List<MenuItemSelectDto> SearchMenuItems(string searchTerm)
        //{
        //    if (string.IsNullOrWhiteSpace(searchTerm))
        //    {
        //        return GetAllAvailableMenuItems();
        //    }

        //    var menuItems = _unitOfWork.MenuItemRepository.GetAll()
        //                        .Where(m => !m.IsDeleted &&
        //                                   (m.ItemName.Contains(searchTerm) ||
        //                                    (m.Description != null && m.Description.Contains(searchTerm))))
        //                        .ToList();

        //    var result = new List<MenuItemSelectDto>();
        //    foreach (var menuItem in menuItems)
        //    {
        //        result.Add(new MenuItemSelectDto
        //        {
        //            Id = menuItem.Id,
        //            ItemName = menuItem.ItemName,
        //            Price = menuItem.Price,
        //            IsAvailable = menuItem.IsAvailable,
        //            CategoryName = menuItem.Category?.CategoryName ?? "Unknown"
        //        });
        //    }

        //    return result;
        //}

        //// Get menu items by price range
        //public List<MenuItemSelectDto> GetMenuItemsByPriceRange(decimal minPrice, decimal maxPrice)
        //{
        //    var menuItems = _unitOfWork.MenuItemRepository.GetAll()
        //                        .Where(m => !m.IsDeleted && m.IsAvailable &&
        //                                   m.Price >= minPrice && m.Price <= maxPrice)
        //                        .ToList();

        //    var result = new List<MenuItemSelectDto>();
        //    foreach (var menuItem in menuItems)
        //    {
        //        result.Add(new MenuItemSelectDto
        //        {
        //            Id = menuItem.Id,
        //            ItemName = menuItem.ItemName,
        //            Price = menuItem.Price,
        //            IsAvailable = menuItem.IsAvailable,
        //            CategoryName = menuItem.Category?.CategoryName ?? "Unknown"
        //        });
        //    }

        //    return result.OrderBy(m => m.Price).ToList();
        //}

        //// Update menu item availability
        //public bool UpdateMenuItemAvailability(int id, bool isAvailable)
        //{
        //    var menuItem = _unitOfWork.MenuItemRepository.GetById(id);
        //    if (menuItem == null || menuItem.IsDeleted)
        //    {
        //        return false;
        //    }

        //    menuItem.IsAvailable = isAvailable;
        //    _unitOfWork.MenuItemRepository.Update(menuItem);
        //    int numberOfRows = _unitOfWork.SaveChanges();
        //    return numberOfRows > 0;
        //}

        //// Delete
        //public bool DeleteMenuItem(int id)
        //{
        //    var menuItem = _unitOfWork.MenuItemRepository.GetById(id);
        //    if (menuItem == null)
        //    {
        //        return false;
        //    }

        //    _unitOfWork.MenuItemRepository.DeleteById(menuItem.Id);
        //    int numberOfRows = _unitOfWork.SaveChanges();
        //    return numberOfRows > 0;
        //} 
        #endregion
        // GetAll
        public IEnumerable<GetAllMenuItemsDto> GetAllMenuItems()
        {
            var menuItems = _unitOfWork.MenuItemRepository.GetAll();

            var menuItemDTO = menuItems.Select(m => new GetAllMenuItemsDto
            {
                Id = m.Id,
                ItemName = m.ItemName,
                Description = m.Description,
                Price = m.Price,
                ImageName = m.ImageName,
                IsAvailable = m.IsAvailable,
                CategoryId = m.CategoryId,
                CreatedBy = m.CreatedBy,
                CreatedOn = m.CreatedOn,
                ModifiedBy = m.ModifiedBy,
                ModifiedOn = m.ModifiedOn,
                IsDeleted = m.IsDeleted,
            });
            return menuItemDTO;
        }

        // GetById
        public GetByIdMenuItemsDto? GetMenuItemById(int id)
        {
            var menuItem = _unitOfWork.MenuItemRepository.GetById(id);
            if (menuItem == null || menuItem.IsDeleted) return null;

            return new GetByIdMenuItemsDto
            {
                Id = menuItem.Id,
                ItemName = menuItem.ItemName,
                Price = menuItem.Price,
                IsAvailable = menuItem.IsAvailable,
                ImageName = menuItem.ImageName,
            };
        }

        // Add
        public int AddMenuItem(CreateMenuItemDto createMenuItemDTO)
        {
            // Validate category exists and is active
            var category = _categoryRepository.GetById(createMenuItemDTO.CategoryId);
            if (category == null || category.IsDeleted || !category.IsActive)
            {
                throw new InvalidOperationException("Category does not exist or is inactive.");
            }

            var menuItem = new MenuItem()
            {
                ItemName = createMenuItemDTO.ItemName,
                Description = createMenuItemDTO.Description,
                Price = createMenuItemDTO.Price,
                IsAvailable = createMenuItemDTO.IsAvailable,
                CategoryId = createMenuItemDTO.CategoryId,
            };

            // Handle image upload
            if (createMenuItemDTO.Image is not null)
            {
                string? imgName = _attachmentService.Upload(createMenuItemDTO.Image, "MenuItems");
                if (imgName is not null)
                {
                    menuItem.ImageName = imgName;
                }
            }

            _unitOfWork.MenuItemRepository.Add(menuItem);
            return _unitOfWork.SaveChanges();
        }

        // Update
        public int UpdateMenuItem(UpdateMenuItemDto updateMenuItemDTO)
        {
            try
            {
                // Validate category exists and is active
                var category = _categoryRepository.GetById(updateMenuItemDTO.CategoryId);
                if (category == null || category.IsDeleted || !category.IsActive)
                {
                    throw new InvalidOperationException("Category does not exist or is inactive.");
                }

                // Fetch the existing tracked entity from the database
                var existingMenuItem = _unitOfWork.MenuItemRepository.GetById(updateMenuItemDTO.Id);

                if (existingMenuItem == null || existingMenuItem.IsDeleted)
                {
                    return 0; // MenuItem not found or deleted
                }

                // Update the properties of the EXISTING tracked entity
                existingMenuItem.ItemName = updateMenuItemDTO.ItemName;
                existingMenuItem.Description = updateMenuItemDTO.Description;
                existingMenuItem.Price = updateMenuItemDTO.Price;
                existingMenuItem.IsAvailable = updateMenuItemDTO.IsAvailable;
                existingMenuItem.CategoryId = updateMenuItemDTO.CategoryId;
                existingMenuItem.ModifiedOn = DateTime.Now;

                // Handle new image upload
                if (updateMenuItemDTO.Image != null)
                {
                    string? imgName = _attachmentService.Upload(updateMenuItemDTO.Image, "MenuItems");
                    if (imgName != null)
                    {
                        existingMenuItem.ImageName = imgName;
                    }
                }
                else if (!string.IsNullOrEmpty(updateMenuItemDTO.ImageName))
                {
                    // Keep the existing image if no new image uploaded
                    existingMenuItem.ImageName = updateMenuItemDTO.ImageName;
                }

                // EF Core is already tracking the entity, just save changes
                return _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"Failed to update menu item: {ex.Message}", ex);
            }
        }

        // Get available menu items for display (اللي هو لو في MenuItems خلصت) -> (دة يجيب اللي موجود مخلصش)
        public List<MenuItemSelectDto> GetAllAvailableMenuItems()
        {
            var menuItems = _unitOfWork.MenuItemRepository.GetAll()
                                .Where(m => m.IsAvailable && !m.IsDeleted)
                                .ToList();

            var result = new List<MenuItemSelectDto>();
            foreach (var menuItem in menuItems)
            {
                result.Add(new MenuItemSelectDto
                {
                    Id = menuItem.Id,
                    ItemName = menuItem.ItemName,
                    Price = menuItem.Price,
                    IsAvailable = menuItem.IsAvailable,
                    CategoryName = menuItem.Category?.CategoryName ?? "Unknown"
                });
            }

            return result;
        }

        // Get MenuItems By Category
        public List<MenuItemsByCategoryDto> GetMenuItemsByCategory(int categoryId)
        {
            var menuItems = _unitOfWork.MenuItemRepository.GetAll()
                                .Where(m => m.CategoryId == categoryId && !m.IsDeleted)
                                .ToList();

            var result = new List<MenuItemsByCategoryDto>();
            foreach (var menuItem in menuItems)
            {
                result.Add(new MenuItemsByCategoryDto
                {
                    Id = menuItem.Id,
                    ItemName = menuItem.ItemName,
                    Description = menuItem.Description,
                    price = menuItem.Price,
                    ImageName = menuItem.ImageName,
                    IsAvailable = menuItem.IsAvailable,
                    CategoryId = menuItem.CategoryId,
                    CategoryName = menuItem.Category?.CategoryName ?? "Unknown"
                });
            }

            return result;
        }

        // Search menu items
        public List<MenuItemSelectDto> SearchMenuItems(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAllAvailableMenuItems();
            }

            var menuItems = _unitOfWork.MenuItemRepository.GetAll()
                                .Where(m => !m.IsDeleted &&
                                           (m.ItemName.Contains(searchTerm) ||
                                            (m.Description != null && m.Description.Contains(searchTerm))))
                                .ToList();

            var result = new List<MenuItemSelectDto>();
            foreach (var menuItem in menuItems)
            {
                result.Add(new MenuItemSelectDto
                {
                    Id = menuItem.Id,
                    ItemName = menuItem.ItemName,
                    Price = menuItem.Price,
                    IsAvailable = menuItem.IsAvailable,
                    CategoryName = menuItem.Category?.CategoryName ?? "Unknown"
                });
            }

            return result;
        }

        // Get menu items by price range
        public List<MenuItemSelectDto> GetMenuItemsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var menuItems = _unitOfWork.MenuItemRepository.GetAll()
                                .Where(m => !m.IsDeleted && m.IsAvailable &&
                                           m.Price >= minPrice && m.Price <= maxPrice)
                                .ToList();

            var result = new List<MenuItemSelectDto>();
            foreach (var menuItem in menuItems)
            {
                result.Add(new MenuItemSelectDto
                {
                    Id = menuItem.Id,
                    ItemName = menuItem.ItemName,
                    Price = menuItem.Price,
                    IsAvailable = menuItem.IsAvailable,
                    CategoryName = menuItem.Category?.CategoryName ?? "Unknown"
                });
            }

            return result.OrderBy(m => m.Price).ToList();
        }

        // Update menu item availability
        public bool UpdateMenuItemAvailability(int id, bool isAvailable)
        {
            var menuItem = _unitOfWork.MenuItemRepository.GetById(id);
            if (menuItem == null || menuItem.IsDeleted)
            {
                return false;
            }

            menuItem.IsAvailable = isAvailable;
            _unitOfWork.MenuItemRepository.Update(menuItem);
            int numberOfRows = _unitOfWork.SaveChanges();
            return numberOfRows > 0;
        }

        // Delete
        public bool DeleteMenuItem(int id)
        {
            var menuItem = _unitOfWork.MenuItemRepository.GetById(id);
            if (menuItem == null)
            {
                return false;
            }

            _unitOfWork.MenuItemRepository.DeleteById(menuItem.Id);
            int numberOfRows = _unitOfWork.SaveChanges();
            return numberOfRows > 0;
        }
    }
}
