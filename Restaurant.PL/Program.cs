using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

            #region DI Container.
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                // success only if the section in the appsettings is ConnectionStrings & the key is Default Connection.
            });
            builder.Services.AddScoped<ICategoryReposatory, CategoryReposatory>();
            builder.Services.AddScoped<IMenuItemReposatory, MenuItemReposatory>();
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
