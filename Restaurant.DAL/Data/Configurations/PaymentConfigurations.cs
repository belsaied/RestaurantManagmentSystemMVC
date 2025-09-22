using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Configurations
{
    internal class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn(1, 1).ValueGeneratedOnAdd();

            #region Base Configurations
            builder.Property(t => t.CreatedOn)
                  .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.ModifiedOn)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            #endregion
        }
    }
}
