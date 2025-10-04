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
                             .HasDefaultValueSql("GETDATE()");

            //builder.Property(c => c.ModifiedOn)
            //    .HasComputedColumnSql("GETDATE()");                 // HasComputedColumnSql every time i open details it takes a new value(I want that behavior with edit only)
            builder.Property(e => e.ModifiedOn)
    .HasDefaultValueSql("GETDATE()");
            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            #endregion
        }
    }
}
