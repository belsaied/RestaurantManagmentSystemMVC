namespace Restaurant.BLL.DTOs.RecipeLinesDtos
{
    public class RecipeLineDto
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string IngredientName { get; set; } = string.Empty;
        public string MenuItemName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
