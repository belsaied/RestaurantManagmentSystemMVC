namespace Restaurant.DAL.Data.Repositories.Interfaces
{
    internal interface IIngredientRepository
    {
        global::System.Int32 AddIngredient(Ingredient ingredient);
        IEnumerable<Ingredient> GetAllIngredient(global::System.Boolean WithTracking = false);
        Ingredient GetIngredientById(global::System.Int32 id);
        global::System.Int32 RemoveIngredient(Ingredient ingredient);
        global::System.Int32 UpdateIngredient(Ingredient ingredient);
    }
}