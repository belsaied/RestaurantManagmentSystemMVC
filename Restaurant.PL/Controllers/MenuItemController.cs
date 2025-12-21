using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AttachmentService;
using Restaurant.BLL.DTOs.MenuItemDTOs;
using Restaurant.BLL.Services.Classes;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.PL.ViewModels.MenuItemVM;

namespace Restaurant.PL.Controllers
{
    [Authorize]

    public class MenuItemController(IMenuItemServices _menuItemService,IHostEnvironment _env, ILogger<MenuItemController> _logger,IAttachmentService _attachmentService) : Controller
    {
        #region Old Controller without Attachment .
        //#region Index
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    var menuItems = _menuItemService.GetAllMenuItems();
        //    return View(menuItems);
        //}
        //#endregion

        //#region Create
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(MenuItemsViewModel menuItemViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            int result = _menuItemService.AddMenuItem(new CreateMenuItemDto()
        //            {
        //                ItemName = menuItemViewModel.ItemName,
        //                Description = menuItemViewModel.Description,
        //                Price = menuItemViewModel.Price,
        //                ImageUrl = menuItemViewModel.ImageUrl,
        //                IsAvailable = menuItemViewModel.IsAvailable,
        //                CategoryId = menuItemViewModel.CategoryId
        //            });

        //            if (result > 0)
        //            {
        //                TempData["Message"] = "Menu Item Created Successfully";
        //                TempData["MessageType"] = "success";
        //                return RedirectToAction(nameof(Index));
        //            }
        //            else
        //            {
        //                TempData["Message"] = "Menu Item Can't be created";
        //                TempData["MessageType"] = "error";
        //                return RedirectToAction(nameof(Index));
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError($"Menu Item Can't be created because {ex.Message}");
        //            TempData["Message"] = "An unexpected error occurred while creating menu item.";
        //            TempData["MessageType"] = "error";
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }

        //    ModelState.AddModelError(string.Empty, "Please check the input data.");
        //    return View(menuItemViewModel);
        //}
        //#endregion

        //#region Details
        //[HttpGet]
        //public IActionResult Details(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var menuItem = _menuItemService.GetMenuItemById(id.Value);
        //    if (menuItem is null) return NotFound();
        //    return View(menuItem);
        //}
        //#endregion

        //#region Edit
        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var menuItem = _menuItemService.GetMenuItemById(id.Value);
        //    if (menuItem is null) return NotFound();

        //    var menuItemVM = new MenuItemsViewModel()
        //    {
        //        ItemName = menuItem.ItemName,
        //        Price = menuItem.Price,
        //        ImageUrl = menuItem.ImageUrl,
        //        IsAvailable = menuItem.IsAvailable
        //    };
        //    return View(menuItemVM);
        //}

        //[HttpPost]
        //public IActionResult Edit([FromRoute] int? id, MenuItemsViewModel menuItemViewModel)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    if (!ModelState.IsValid) return View(menuItemViewModel);

        //    try
        //    {
        //        int result = _menuItemService.UpdateMenuItem(new UpdateMenuItemDto()
        //        {
        //            Id = id.Value,
        //            ItemName = menuItemViewModel.ItemName,
        //            Description = menuItemViewModel.Description,
        //            Price = menuItemViewModel.Price,
        //            ImageUrl = menuItemViewModel.ImageUrl,
        //            IsAvailable = menuItemViewModel.IsAvailable,
        //            CategoryId = menuItemViewModel.CategoryId
        //        });

        //        if (result > 0)
        //        {
        //            TempData["Message"] = "Menu Item Updated Successfully";
        //            TempData["MessageType"] = "success";
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Menu Item Can't be Updated");
        //            return View(menuItemViewModel);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (_env.IsDevelopment())
        //        {
        //            _logger.LogError($"Menu Item Can't be updated because {ex.Message}");
        //            return View(menuItemViewModel);
        //        }
        //        else
        //        {
        //            _logger.LogError($"Menu Item Can't be updated because {ex}");
        //            return View("ErrorView", ex);
        //        }
        //    }
        //}
        //#endregion

        //#region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var menuItem = _menuItemService.GetMenuItemById(id.Value);
        //    if (menuItem == null) return NotFound();
        //    return View(menuItem);
        //}

        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    if (id == 0) return BadRequest();
        //    try
        //    {
        //        bool isDeleted = _menuItemService.DeleteMenuItem(id);
        //        if (isDeleted)
        //        {
        //            TempData["Message"] = "Menu Item Deleted Successfully";
        //            TempData["MessageType"] = "success";
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Menu Item can't be Deleted");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (_env.IsDevelopment())
        //        {
        //            _logger.LogError($"Menu Item Can't be deleted because {ex.Message}");
        //        }
        //        else
        //        {
        //            _logger.LogError($"Menu Item Can't be deleted because {ex}");
        //            return View("ErrorView", ex);
        //        }
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
        //#endregion

        //#region Update Availability
        //[HttpPost]
        //public IActionResult ToggleAvailability(int id)
        //{
        //    try
        //    {
        //        var menuItem = _menuItemService.GetMenuItemById(id);
        //        if (menuItem == null)
        //        {
        //            TempData["Message"] = "Menu Item not found";
        //            TempData["MessageType"] = "error";
        //            return RedirectToAction(nameof(Index));
        //        }

        //        bool result = _menuItemService.UpdateMenuItemAvailability(id, !menuItem.IsAvailable);
        //        if (result)
        //        {
        //            TempData["Message"] = "Availability Updated Successfully";
        //            TempData["MessageType"] = "success";
        //        }
        //        else
        //        {
        //            TempData["Message"] = "Failed to update availability";
        //            TempData["MessageType"] = "error";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error updating availability: {ex.Message}");
        //        TempData["Message"] = "An error occurred";
        //        TempData["MessageType"] = "error";
        //    }

        //    return RedirectToAction(nameof(Index));
        //}
        //#endregion 
        #endregion
        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var menuItems = _menuItemService.GetAllMenuItems();
            return View(menuItems);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MenuItemsViewModel menuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = _menuItemService.AddMenuItem(new CreateMenuItemDto()
                    {
                        ItemName = menuItemViewModel.ItemName,
                        Description = menuItemViewModel.Description,
                        Price = menuItemViewModel.Price,
                        IsAvailable = menuItemViewModel.IsAvailable,
                        CategoryId = menuItemViewModel.CategoryId,
                        Image = menuItemViewModel.Image
                    });

                    if (result > 0)
                    {
                        TempData["Message"] = "Menu Item Created Successfully";
                        TempData["MessageType"] = "success";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Message"] = "Menu Item Can't be created";
                        TempData["MessageType"] = "error";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Menu Item Can't be created because {ex.Message}");
                    TempData["Message"] = "An unexpected error occurred while creating menu item.";
                    TempData["MessageType"] = "error";
                    return RedirectToAction(nameof(Index));
                }
            }

            ModelState.AddModelError(string.Empty, "Please check the input data.");
            return View(menuItemViewModel);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var menuItem = _menuItemService.GetMenuItemById(id.Value);
            if (menuItem is null) return NotFound();
            return View(menuItem);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();

            // Get the full menu item details
            var allMenuItems = _menuItemService.GetAllMenuItems();
            var menuItem = allMenuItems.FirstOrDefault(m => m.Id == id.Value);

            if (menuItem == null || menuItem.IsDeleted)
            {
                TempData["Message"] = "Menu Item not found";
                TempData["MessageType"] = "error";
                return RedirectToAction(nameof(Index));
            }

            // Map ALL properties to the view model
            var menuItemVM = new MenuItemsViewModel()
            {
                Id = menuItem.Id,
                ItemName = menuItem.ItemName,
                Description = menuItem.Description,  // ✅ Include description
                Price = menuItem.Price,
                ImageName = menuItem.ImageName,
                IsAvailable = menuItem.IsAvailable,
                CategoryId = menuItem.CategoryId     // ✅ CRITICAL: Include CategoryId
            };

            return View(menuItemVM);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, MenuItemsViewModel menuItemViewModel)
        {
            if (!id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return View(menuItemViewModel);

            try
            {
                // Get existing menuItem to preserve image if not updating
                var existingMenuItem = _menuItemService.GetMenuItemById(id.Value);

                if (existingMenuItem == null)
                {
                    TempData["Message"] = "Menu Item not found";
                    TempData["MessageType"] = "error";
                    return RedirectToAction(nameof(Index));
                }

                // Handle old image deletion if new image is uploaded
                if (menuItemViewModel.Image != null && !string.IsNullOrEmpty(existingMenuItem.ImageName))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\Files\\MenuItems", existingMenuItem.ImageName);
                    _attachmentService.Delete(oldImagePath);
                }

                // Create the update DTO with all required fields
                var updateDto = new UpdateMenuItemDto()
                {
                    Id = id.Value,
                    ItemName = menuItemViewModel.ItemName,
                    Description = menuItemViewModel.Description,
                    Price = menuItemViewModel.Price,
                    IsAvailable = menuItemViewModel.IsAvailable,
                    CategoryId = menuItemViewModel.CategoryId,
                    Image = menuItemViewModel.Image,
                    // CRITICAL: Preserve existing image if no new image uploaded
                    ImageName = menuItemViewModel.Image == null ? existingMenuItem.ImageName : null
                };

                int result = _menuItemService.UpdateMenuItem(updateDto);

                if (result > 0)
                {
                    TempData["Message"] = "Menu Item Updated Successfully";
                    TempData["MessageType"] = "success";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Menu Item Can't be Updated");
                    return View(menuItemViewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Menu Item Can't be updated: {ex.Message}");
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View(menuItemViewModel);
            }
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var menuItem = _menuItemService.GetMenuItemById(id.Value);
            if (menuItem == null) return NotFound();
            return View(menuItem);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool isDeleted = _menuItemService.DeleteMenuItem(id);
                if (isDeleted)
                {
                    TempData["Message"] = "Menu Item Deleted Successfully";
                    TempData["MessageType"] = "success";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Menu Item can't be Deleted");
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    _logger.LogError($"Menu Item Can't be deleted because {ex.Message}");
                }
                else
                {
                    _logger.LogError($"Menu Item Can't be deleted because {ex}");
                    return View("ErrorView", ex);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update Availability
        [HttpPost]
        public IActionResult ToggleAvailability(int id)
        {
            try
            {
                var menuItem = _menuItemService.GetMenuItemById(id);
                if (menuItem == null)
                {
                    TempData["Message"] = "Menu Item not found";
                    TempData["MessageType"] = "error";
                    return RedirectToAction(nameof(Index));
                }

                bool result = _menuItemService.UpdateMenuItemAvailability(id, !menuItem.IsAvailable);
                if (result)
                {
                    TempData["Message"] = "Availability Updated Successfully";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Failed to update availability";
                    TempData["MessageType"] = "error";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating availability: {ex.Message}");
                TempData["Message"] = "An error occurred";
                TempData["MessageType"] = "error";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
