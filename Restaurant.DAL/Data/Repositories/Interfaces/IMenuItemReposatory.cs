using Restaurant.DAL.Models;

namespace Restaurant.DAL.Data.Repositories.Interfaces
{
    public interface IMenuItemReposatory
    {
        int Add(MenuItem menuItem);
        int Delete(int id);
        IEnumerable<MenuItem> GetAll(bool withTracking = false);
        MenuItem? GetById(int id);
        int Update(MenuItem menuItem);
    }
}