using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Interfaces;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Repositories.Classes
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : baseEntity
    {
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int DeleteById(int id)
        {
            var entity= _dbContext.Set<TEntity>().FirstOrDefault(x=>x.Id==id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _dbContext.Set<TEntity>().Update(entity);
            }
            return 0;
        }


        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        { 
           return withTracking
            ? _dbContext.Set<TEntity>().Where(p => !p.IsDeleted).ToList()
            : _dbContext.Set<TEntity>().AsNoTracking().Where(p => !p.IsDeleted).ToList();

        }

        public TEntity? GetById(int id)
        {
          return  _dbContext.Set<TEntity>().FirstOrDefault(p => p.Id == id);
        }

        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }

    }
}
