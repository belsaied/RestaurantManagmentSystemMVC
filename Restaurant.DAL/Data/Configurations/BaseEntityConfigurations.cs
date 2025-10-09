using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Configurations
{
    public class BaseEntityConfigurations<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : baseEntity
    {
       

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {

            builder.Property(m => m.CreatedOn)
               .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.ModifiedOn)
            .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
