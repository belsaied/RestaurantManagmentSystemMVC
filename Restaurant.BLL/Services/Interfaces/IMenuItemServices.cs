using Restaurant.BLL.DTOs.MenuItemDTOs;

namespace Restaurant.BLL.Services.Interfaces
{
    public interface IMenuItemServices
    {
        int AddMenuItem(CreateMenuItemDto createMenuItemDTO);
        bool DeleteMenuItem(int id);
        List<MenuItemSelectDto> GetAllAvailableMenuItems();
        IEnumerable<GetAllMenuItemsDto> GetAllMenuItems();
        GetByIdMenuItemsDto? GetMenuItemById(int id);
        List<MenuItemsByCategoryDto> GetMenuItemsByCategory(int categoryId);
        List<MenuItemSelectDto> GetMenuItemsByPriceRange(decimal minPrice, decimal maxPrice);
        List<MenuItemSelectDto> SearchMenuItems(string searchTerm);
        int UpdateMenuItem(UpdateMenuItemDto updateMenuItemDTO);
        bool UpdateMenuItemAvailability(int id, bool isAvailable);
    }
}