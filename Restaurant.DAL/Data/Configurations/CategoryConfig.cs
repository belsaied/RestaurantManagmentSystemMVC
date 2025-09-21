using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Primary Key
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();


            // Base Entity Properties
            //builder.Property(c => c.CreatedBy)
            //    .IsRequired();

            builder.Property(c => c.CreatedOn)
                .HasDefaultValueSql("GETUTCDATE()");

            //builder.Property(c => c.ModifiedBy)
            //    .IsRequired();

            builder.Property(c => c.ModifiedOn)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);


            // Category Specific Properties
            builder.Property(c => c.CategoryName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(c => c.Description)
                .HasMaxLength(500)
                .HasColumnType("nvarchar(500)");

            builder.Property(c => c.DisplayOrder)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(c => c.IsActive)
                .IsRequired()
                .HasDefaultValue(true);



            // Relationships
            builder.HasMany(c => c.MenuItems)
                .WithOne(m => m.Category)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
               
        }
    }
}
