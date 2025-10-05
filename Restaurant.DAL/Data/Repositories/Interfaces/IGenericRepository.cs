using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : baseEntity
    {

        int Add(TEntity entity);
        int DeleteById(int id);
        IEnumerable<TEntity>? GetAll(bool WithTracking = false);
        TEntity? GetById(int Id);
        int Update(TEntity entity);


    }
}
