namespace Restaurant.BLL.DTOs.RecipeLinesDtos
{
    public class RecipeLineSelectDto
    {
        public int Id { get; set; }
        public string MenuItemName { get; set; } = string.Empty;
        public string IngredientName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}
