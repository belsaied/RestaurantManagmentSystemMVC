using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Restaurant.BLL.AttachmentService;
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
            #region MVC Config
            builder.Services.AddControllersWithViews(options =>
               {
                   options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
               }); 
            #endregion
            #region Contexts
            builder.Services.AddDbContext<AppDbContext>(options =>
               {
                   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                   // success only if the section in the appsettings is ConnectionStrings & the key is Default Connection.
               }); 
            #endregion
            #region Repos
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
            builder.Services.AddScoped<IRecipeLineRepository, RecipeLineRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion
            #region BLL Services
            builder.Services.AddScoped<IPaymentServices, PaymentServices>();
            builder.Services.AddScoped<IOrderItemsServices, OrderItemsServices>();
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<IIngredientServices, IngredientServices>();
            
            builder.Services.AddScoped<ICustomerService,CustomerService>();
            builder.Services.AddScoped<IRecipeLineServices, RecipeLineServices>();
           
            builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            #endregion
            #region AutoMapper
            builder.Services.AddAutoMapper(mapping => mapping.AddProfile(new MappingProfile())); 
            #endregion
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
