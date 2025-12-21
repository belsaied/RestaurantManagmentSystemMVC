namespace Restaurant.BLL.DTOs.RecipeLinesDtos
{
    public class RecipeLineDetailsDto
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string IngredientName { get; set; } = string.Empty;
        public string MenuItemName { get; set; } = string.Empty;
        public int IngredientId { get; set; }
        public int MenuId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
