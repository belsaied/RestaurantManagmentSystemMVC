using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Configurations
{
    internal class PaymentConfigurations : BaseEntityConfigurations<Payment>
    {
        public new void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn(1, 1).ValueGeneratedOnAdd();

            #region Base Configurations
            base.Configure(builder);


            #endregion


        }
    }
}
