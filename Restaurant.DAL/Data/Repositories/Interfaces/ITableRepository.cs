namespace Restaurant.DAL.Data.Repositories.Interfaces
{
    public interface ITableRepository
    {
        int Add(Table table);
        int Delete(int id);
        IEnumerable<Table> GetAll(bool withTracking = false);
        Table? GetById(int id);
        int Update(Table table);
    }
}