using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Restaurant.DAL.Data.Repositories.Classes
{
    internal class IngredientRepository(AppDbContext _DbContext) : IIngredientRepository
    {

        //Get all ingredients

        public IEnumerable<Ingredient> GetAllIngredient(bool WithTracking = false)
        {
            if (!WithTracking)
            {
                return _DbContext.Ingredients.AsNoTracking().ToList();
            }

            return _DbContext.Ingredients.ToList();
        }

        //Get by Id

        public Ingredient? GetIngredientById(int id)
        {
            return _DbContext.Ingredients.Find(id);
        }

        //Add Ingredient
        public int AddIngredient(Ingredient ingredient)
        {
            _DbContext.Ingredients.Add(ingredient);
            return _DbContext.SaveChanges();
        }

        //Remove Ingredient
        public int RemoveIngredient(Ingredient ingredient)
        {


            _DbContext.Ingredients.Remove(ingredient);


            return _DbContext.SaveChanges();
        }

        //Update Ingredient
        public int UpdateIngredient(Ingredient ingredient)
        {
            _DbContext.Ingredients.Update(ingredient);
            return _DbContext.SaveChanges();
        }
    }
}
