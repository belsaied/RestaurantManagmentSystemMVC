using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Configurations
{
    internal class RecipeLineConfiguration : IEntityTypeConfiguration<RecipeLine>
    {
        public void Configure(EntityTypeBuilder<RecipeLine> builder)
        {
           builder.HasKey(rl => rl.Id);

            

            #region Relations

            builder.HasOne(rl => rl.Ingredient)
                .WithMany(i => i.RecipeLines)
                .HasForeignKey(rl => rl.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rl => rl.MenuItem)
                .WithMany(mi => mi.RecipeLines)
                .HasForeignKey(rl => rl.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion    
        }
    }
}
