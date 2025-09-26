namespace Restaurant.DAL.Data.Repositories.Interfaces
{
    public interface IRecipeLineRepository
    {
        global::System.Int32 AddRecipeLine(RecipeLine recipeLine);
        IEnumerable<RecipeLine> GetAllRecipeLines(global::System.Boolean WithTracking = false);
        RecipeLine GetRecipeLineById(global::System.Int32 id);
        global::System.Int32 RemoveRecipeLine(RecipeLine recipeLine);
        global::System.Int32 UpdateRecipeLine(RecipeLine recipeLine);
    }
}