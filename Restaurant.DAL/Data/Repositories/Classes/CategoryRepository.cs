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
    public class CategoryRepository(AppDbContext _dbContext) : ICategoryReposatory
    {
        // 5 CRUD Operations 
        // GetAll
        public IEnumerable<Category> GetAll(bool withTracking = false)
        {
            return withTracking
            ? _dbContext.Categories.ToList()
            : _dbContext.Categories.AsNoTracking().ToList();
        }
        // GetById
        public Category? GetById(int id)
        {
            return _dbContext.Categories.Find(id);
        }
        // Add
        public int Add(Category category)
        {
            _dbContext.Add(category);
            return _dbContext.SaveChanges();
        }
        // Update
        public int Update(Category category)
        {
            _dbContext.Update(category);
            return _dbContext.SaveChanges();
        }
        // Delete (soft Delete)
        public int Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category != null)
            {
                category.IsDeleted = true;
                _dbContext.Categories.Update(category);
                return _dbContext.SaveChanges();
            }
            return 0;
        }
    }
}
