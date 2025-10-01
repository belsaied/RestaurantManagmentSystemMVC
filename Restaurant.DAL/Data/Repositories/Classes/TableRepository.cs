using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Repositories.Classes
{
    public class TableRepository(AppDbContext _dbContext) : ITableRepository
    {
        public IEnumerable<Table> GetAll(bool withTracking = false) =>
             withTracking
            ? _dbContext.Tables.Where(t => !t.IsDeleted).ToList()
            : _dbContext.Tables.AsNoTracking().Where(t => !t.IsDeleted).ToList();


        public Table? GetById(int id) =>
             _dbContext.Tables.Find(id);


        public int Add(Table table)
        {
            _dbContext.Add(table);
            return _dbContext.SaveChanges();
        }
        public int Update(Table table)
        {
            _dbContext.Update(table);
            return _dbContext.SaveChanges();
        }
        public int Delete(int id)
        {
            var table = GetById(id);
            if (table != null)
            {
                table.IsDeleted = true;
                Update(table);

            }
            return 0;
        }

    }
}
//Ahmed
