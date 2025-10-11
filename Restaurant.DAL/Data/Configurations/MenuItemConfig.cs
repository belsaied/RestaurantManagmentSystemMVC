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
    public class MenuItemConfig : BaseEntityConfigurations<MenuItem>
    {
        public new void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            // Primary Key
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).UseIdentityColumn();
                

            // Base Entity Properties
            //builder.Property(m => m.CreatedBy)
            //    .IsRequired();

           

            //builder.Property(m => m.ModifiedBy)
            //    .IsRequired();


            // MenuItem Specific Properties
            builder.Property(m => m.ItemName)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("nvarchar(150)");

            builder.Property(m => m.Description)
                .HasMaxLength(1000)
                .HasColumnType("nvarchar(1000)");

            builder.Property(m => m.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)")
                .HasPrecision(10, 2);

            builder.Property(m => m.ImageName)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.Property(m => m.IsAvailable)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(m => m.CategoryId)
                .IsRequired();



            // Relationships
            builder.HasOne(m => m.Category)
                .WithMany(c => c.MenuItems)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);



            //base
            base.Configure(builder);


        }
    }
}
