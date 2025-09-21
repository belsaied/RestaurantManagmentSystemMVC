using Restaurant.DAL.Models;

namespace Restaurant.DAL.Data.Repositories.Interfaces
{
    public interface ICategoryReposatory
    {
        int Add(Category category);
        int Delete(int id);
        IEnumerable<Category> GetAll(bool withTracking = false);
        Category? GetById(int id);
        int Update(Category category);
    }
}