using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Contexts
{
    // Applying Dependency Injection.
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // to work with Configurations when we decide to add it .
        }
        #region DbSets<>
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; } 
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeLine> RecipeLines { get; set; }
        public DbSet<Order> Orders { get; set; }
        #endregion
    }
}
