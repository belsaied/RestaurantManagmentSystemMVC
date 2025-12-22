using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Configurations
{
    internal class OrderItmeConfigurations : BaseEntityConfigurations<OrderItems>
    {
        public new void Configure(EntityTypeBuilder<OrderItems> builder)
        {

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn(1, 1);

            // Remove computed column - we'll calculate in code
            builder.Property(o => o.TotalPrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.UnitPrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Quantity)
                   .IsRequired();

            #region Base Configurations
            base.Configure(builder);

            #endregion
        }
    }
}
