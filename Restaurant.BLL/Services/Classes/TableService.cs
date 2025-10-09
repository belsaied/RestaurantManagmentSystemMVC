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
    public class TableService(IUnitOfWork _unitOfWork, IMapper _mapper) : ITableService
    {
        public IEnumerable<TableDto> GetAll(bool withTracking = false) =>
       _mapper.Map<IEnumerable<Table>, IEnumerable<TableDto>>(_unitOfWork.TableRepository.GetAll(withTracking));

        public TabelDetailsDto GetById(int id) =>
            _mapper.Map<Table, TabelDetailsDto>(_unitOfWork.TableRepository.GetById(id));

        public int Add(CreatedTableDto createdTable) 
        {
            _unitOfWork.TableRepository.Add(_mapper.Map<CreatedTableDto, Table>(createdTable));
            return _unitOfWork.SaveChanges();
        }
        public int Update(UpdatedTableDto updatedTable)
        { 
            _unitOfWork.TableRepository.Update(_mapper.Map<UpdatedTableDto, Table>(updatedTable));
            return _unitOfWork.SaveChanges();
        }
        public bool Delete(int id)
        {
            _unitOfWork.TableRepository.DeleteById(id) ;
            return _unitOfWork.SaveChanges() > 0 ? true : false;
        }
    }
}
