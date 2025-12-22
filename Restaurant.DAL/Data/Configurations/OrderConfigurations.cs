using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Configurations
{
    public class OrderConfigurations : BaseEntityConfigurations<Order>
    {
        public new void Configure(EntityTypeBuilder<Order> builder)
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
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .UseIdentityColumn(1, 1);

            // Configure decimal properties with precision
            builder.Property(x => x.SubTotal)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ServiceTax)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Total)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Discount)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(x => x.PaymentStatus)
                   .HasColumnType("varchar(20)");

            builder.Property(x => x.OrderType)
                   .HasColumnType("varchar(20)");

            builder.Property(x => x.Status)
                   .HasColumnType("varchar(20)")
                   .HasDefaultValue("Pending");

            builder.Property(x => x.CreatedBy)
                   .HasColumnType("varchar(50)");

            builder.Property(x => x.ModifiedBy)
                   .HasColumnType("varchar(50)");

            builder.Property(x => x.OrderDate)
                   .HasDefaultValueSql("GETDATE()");

            #region Relationships
            builder.HasOne(x => x.NavTable)
                   .WithMany(c => c.NavOrders)
                   .HasForeignKey(z => z.TableId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.NavCustomer)
                   .WithMany(c => c.NavOrders)
                   .HasForeignKey(z => z.CustomerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.NavPayments)
                   .WithOne(c => c.NavOrder)
                   .HasForeignKey(p => p.OrderId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.NavOrderItems)
                   .WithOne(c => c.NavOrder)
                   .HasForeignKey(p => p.OrderId)
                   .OnDelete(DeleteBehavior.NoAction);
            #endregion

            base.Configure(builder);




        }
    }
}