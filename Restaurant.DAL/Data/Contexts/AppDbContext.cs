using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public class AppDbContext(DbContextOptions<AppDbContext> options) :IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // to work with Configurations when we decide to add it .
            base.OnModelCreating(modelBuilder); // Important for Identity Package
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries<baseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // ✅ Prevent changing CreatedOn by mistake
                    entry.Property(x => x.CreatedOn).IsModified = false;

                    // ✅ Update ModifiedOn every time an entity is edited
                    entry.Entity.ModifiedOn = DateTime.Now;
                }
            }
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
