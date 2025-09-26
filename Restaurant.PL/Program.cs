using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Restaurant.BLL.Mappings;
using Restaurant.BLL.Services.Classes;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Classes;
using Restaurant.DAL.Data.Repositories.Interfaces;

namespace Restaurant.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                // success only if the section in the appsettings is ConnectionStrings & the key is Default Connection.
            });
            #region Repos
            builder.Services.AddScoped<ICategoryReposatory, CategoryRepository>();
            builder.Services.AddScoped<IMenuItemReposatory, MenuItemRepository>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
            builder.Services.AddScoped<IRecipeLineRepository, RecipeLineRepository>();

            #endregion
            #region BLL Services
            builder.Services.AddScoped<IPaymentServices, PaymentServices>();
            builder.Services.AddScoped<IOrderItemsServices, OrderItemsServices>();
            builder.Services.AddScoped<ITableService, TableService>();
            #endregion

            builder.Services.AddAutoMapper(mapping=>mapping.AddProfile(new MappingProfile()));
            #endregion



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
