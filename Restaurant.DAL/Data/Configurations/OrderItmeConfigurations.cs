using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Configurations
{
    internal class OrderItmeConfigurations : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn(20, 1);

            builder
           .Property(o => o.TotalPrice)
           .HasComputedColumnSql("[UnitPrice] * [Quantity]");

            #region Base Configurations
            builder.Property(t => t.CreatedOn)
                  .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.ModifiedOn)
                .HasComputedColumnSql("GETDATE()");

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            #endregion
        }
    }
}
