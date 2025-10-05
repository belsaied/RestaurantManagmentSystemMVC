using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Repositories.Classes
{
    public class RecipeLineRepository(AppDbContext _DbContext) : GenericRepository<RecipeLine>(_DbContext),IRecipeLineRepository
    {

        
    }
}
