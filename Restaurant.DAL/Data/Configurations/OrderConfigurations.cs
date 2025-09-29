using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            #region problem with database

            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            //builder.Property(x => x.PaymentStatus).HasColumnType("varchar(20)");
            //builder.Property(x => x.OrderType).HasColumnType("varchar(20)");
            //builder.Property(x => x.Status).HasColumnType("varchar(20)");
            //builder.Property(x => x.CreatedBy).HasColumnType("varchar(50)");
            //builder.Property(x => x.CreatedOn).HasColumnType("datetime2").HasDefaultValueSql("GETDATE()");
            //builder.Property(x => x.ModifiedBy).HasColumnType("varchar(50)");
            //builder.Property(x => x.ModifiedOn).HasColumnType("datetime2").HasComputedColumnSql("GETDATE()"); ;
            //builder.Property(x => x.IsDeleted).HasColumnType("bit").HasDefaultValueSql("false"); 
            #endregion
            #region New One
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .UseIdentityColumn(1, 1);

            builder.Property(x => x.PaymentStatus)
                   .HasColumnType("varchar(20)");

            builder.Property(x => x.OrderType)
                   .HasColumnType("varchar(20)");

            builder.Property(x => x.Status)
                   .HasColumnType("varchar(20)");

            builder.Property(x => x.CreatedBy)
                   .HasColumnType("varchar(50)");

            builder.Property(x => x.CreatedOn)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.ModifiedBy)
                   .HasColumnType("varchar(50)");

            builder.Property(x => x.ModifiedOn)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()"); // ✅ بدال HasComputedColumnSql

            builder.Property(x => x.IsDeleted)
                   .HasColumnType("bit")
                   .HasDefaultValue(0); // ✅ بدال HasDefaultValueSql("false") 
            #endregion

            #region Relationship
            builder.HasOne(x => x.NavTable).WithMany(c => c.NavOrders).HasForeignKey(z => z.TableId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.NavCustomer).WithMany(c => c.NavOrders).HasForeignKey(z => z.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.NavPayments).WithOne(c => c.NavOrder).HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.NavOrderItems).WithOne(c => c.NavOrder).HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion




        }
    }
}