using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.DAL.Models;
namespace Restaurant.DAL.Data.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        #region Old Part it has a problem With the update-database Command
        //public void Configure(EntityTypeBuilder<Customer> builder)
        //{
        //    builder.HasKey(x => x.Id);
        //    builder.Property(x => x.Id).UseIdentityColumn(1, 1).ValueGeneratedOnAdd();
        //    builder.Property(x => x.FirstName).HasColumnType("varchar(50)").IsRequired();
        //    builder.Property(x => x.LastName).HasColumnType("varchar(50)").IsRequired();
        //    builder.Property(x => x.Phone).HasColumnType("varchar(50)").IsRequired();
        //    builder.Property(x => x.Email).HasColumnType("varchar(50)").IsRequired();
        //    builder.Property(x => x.LoyaltyPoints).HasColumnType("int");
        //    builder.Property(x => x.IsActive).HasColumnType("bit");
        //    builder.Property(x=>x.CreatedBy).HasColumnType("varchar(50)");
        //    builder.Property(x => x.CreatedOn).HasColumnType("datetime2").HasDefaultValueSql("GETDATE()");
        //    builder.Property(x => x.ModifiedBy).HasColumnType("varchar(50)");
        //    builder.Property(x => x.ModifiedOn).HasColumnType("datetime2").HasComputedColumnSql("GETDATE()"); ;
        //    builder.Property(x => x.IsDeleted).HasColumnType("bit").HasDefaultValueSql("false");


        //} 
        #endregion
        #region new one
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .UseIdentityColumn(1, 1)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.Property(x => x.LastName)
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.Property(x => x.Phone)
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.Property(x => x.Email)
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.Property(x => x.LoyaltyPoints)
                   .HasColumnType("int");

            builder.Property(x => x.IsActive)
                   .HasColumnType("bit")
                   .HasDefaultValue(true); // الافتراضي فعال (تقدر تخليه false لو حاب)

            builder.Property(x => x.CreatedBy)
                   .HasColumnType("varchar(50)");

            builder.Property(x => x.CreatedOn)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.ModifiedBy)
                   .HasColumnType("varchar(50)");

            builder.Property(x => x.ModifiedOn)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");
            // ملاحظة: هذا يحط قيمة وقت الإدخال فقط، إذا تبغى يتحدث تلقائياً عند كل UPDATE لازم تعمل Trigger في SQL

            builder.Property(x => x.IsDeleted)
                   .HasColumnType("bit")
                   .HasDefaultValue(false); // هنا نستخدم false بشكل صحيح
        }


        #endregion
    }
}