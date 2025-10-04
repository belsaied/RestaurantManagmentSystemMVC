using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Configurations
{
    internal class IngredientConfiguration:BaseEntityConfigurations<Ingredient>
    {
        public new void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(I => I.Id);



            #region Relations


            #endregion

            #region Base 
            base.Configure(builder);

            #endregion
        }
    }
}
