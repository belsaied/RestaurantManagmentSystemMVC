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
    public class MenuItemReposatory(AppDbContext _dbContext) : IMenuItemReposatory
    {
        // GetAll
        public IEnumerable<MenuItem> GetAll(bool withTracking = false)
        {
            return withTracking
            ? _dbContext.MenuItems.ToList()
            : _dbContext.MenuItems.AsNoTracking().ToList();
        }
        //GetById
        public MenuItem? GetById(int id) => _dbContext.MenuItems.Find(id);

        //Add
        public int Add(MenuItem menuItem)
        {
            _dbContext.Add(menuItem);
            return _dbContext.SaveChanges();
        }
        //Update
        public int Update(MenuItem menuItem)
        {
            _dbContext.Update(menuItem);
            return _dbContext.SaveChanges();
        }
        //delete
        public int Delete(int id)
        {
            var menu = _dbContext.MenuItems.Find(id);
            if (menu != null)
            {
                menu.IsDeleted = true;
                _dbContext.MenuItems.Update(menu);
                return _dbContext.SaveChanges();
            }
            return 0;
        }
    }
}
