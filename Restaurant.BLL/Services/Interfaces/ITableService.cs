using Restaurant.BLL.DTOs.TableModule;

namespace Restaurant.BLL.Services.Interfaces
{
    public interface ITableService
    {
        int Add(CreatedTableDto createdTable);
        bool Delete(int id);
        IEnumerable<TableDto> GetAll(bool withTracking = false);
        TabelDetailsDto GetById(int id);
        int Update(UpdatedTableDto updatedTable);
    }
}