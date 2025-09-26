using AutoMapper;
using Restaurant.BLL.DTOs.TableModule;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.DAL.Data.Repositories.Interfaces;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services.Classes
{
    public class TableService(ITableRepository _tableRepo, IMapper _mapper) : ITableService
    {
        public IEnumerable<TableDto> GetAll(bool withTracking = false) =>
       _mapper.Map<IEnumerable<Table>, IEnumerable<TableDto>>(_tableRepo.GetAll(withTracking));

        public TabelDetailsDto GetById(int id) =>
            _mapper.Map<Table, TabelDetailsDto>(_tableRepo.GetById(id));

        public int Add(CreatedTableDto createdTable) =>
            _tableRepo.Add(_mapper.Map<CreatedTableDto, Table>(createdTable));

        public int Update(UpdatedTableDto updatedTable) =>
            _tableRepo.Update(_mapper.Map<UpdatedTableDto, Table>(updatedTable));

        public bool Delete(int id) => _tableRepo.Delete(id) > 0;
    }
}
