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
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void DeleteById(int id)
        {
            var entity= _dbContext.Set<TEntity>().FirstOrDefault(x=>x.Id==id);
            if (entity != null && entity.IsDeleted==false)
            {
                entity.IsDeleted = true;
                _dbContext.Set<TEntity>().Update(entity);
            }
             
        }


        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        { 
           return withTracking
            ? _dbContext.Set<TEntity>().Where(p => p.IsDeleted==false).ToList()
            : _dbContext.Set<TEntity>().AsNoTracking().Where(p => !p.IsDeleted).ToList();

        }

        public TEntity? GetById(int id)
        {
            return _dbContext.Set<TEntity>()
                          .FirstOrDefault(e => e.Id == id && !e.IsDeleted);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            
        }

    }
}
