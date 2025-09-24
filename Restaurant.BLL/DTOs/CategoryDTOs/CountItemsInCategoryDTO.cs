using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.CategoryDTOs
{
    public class CountItemsInCategoryDTO :GetAllCategoriesDTO
    {
        public int MenuItemsCount { get; set; }
    }
}
