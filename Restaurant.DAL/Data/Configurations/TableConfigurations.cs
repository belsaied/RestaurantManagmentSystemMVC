using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Configurations
{
    internal class TableConfigurations : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t=>t.Id).UseIdentityColumn(100,10).ValueGeneratedOnAdd();

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
