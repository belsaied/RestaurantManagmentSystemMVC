using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Repositories.Classes
{
    internal class RecipeLineRepository(AppDbContext _DbContext) : IRecipeLineRepository
    {

        //Get All RecipeLines
        public IEnumerable<RecipeLine> GetAllRecipeLines(bool WithTracking = false)
        {
            if (WithTracking == false)
            {
                return _DbContext.RecipeLines.AsNoTracking().ToList();
            }

            return _DbContext.RecipeLines.ToList();
        }

        //Get RecipeLine by Id

        public RecipeLine? GetRecipeLineById(int id) => _DbContext.RecipeLines.Find(id);

        //Add RecipeLine

        public int AddRecipeLine(RecipeLine recipeLine)
        {
            _DbContext.RecipeLines.Add(recipeLine);
            return _DbContext.SaveChanges();
        }

        //Remove RecipeLine
        public int RemoveRecipeLine(RecipeLine recipeLine)
        {
            _DbContext.RecipeLines.Remove(recipeLine);
            return _DbContext.SaveChanges();
        }

        //Update RecipeLine
        public int UpdateRecipeLine(RecipeLine recipeLine)
        {
            _DbContext.RecipeLines.Update(recipeLine);
            return _DbContext.SaveChanges();
        }
    }
}
