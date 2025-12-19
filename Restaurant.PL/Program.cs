using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 using Microsoft.Extensions.Options;
 using Restaurant.BLL.AttachmentService;
 using Restaurant.BLL.Mappings;
 using Restaurant.BLL.SendEmailService;
 using Restaurant.BLL.Services.Classes;
 using Restaurant.BLL.Services.Interfaces;
 using Restaurant.DAL.Data.Contexts;
 using Restaurant.DAL.Data.Repositories.Classes;
 using Restaurant.DAL.Data.Repositories.Interfaces;
 using Restaurant.DAL.DataSeeding;
 using Restaurant.DAL.Models;


namespace Restaurant.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
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
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IOrderItemsServices, OrderItemsServices>();
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<IIngredientServices, IngredientServices>();
            
            builder.Services.AddScoped<ICustomerService,CustomerService>();
            builder.Services.AddScoped<IRecipeLineServices, RecipeLineServices>();
           
            builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            
            builder.Services.AddScoped<ISendEmailService,SendEmailService>();    
            #endregion
            #region AutoMapper
            builder.Services.AddAutoMapper(mapping => mapping.AddProfile(new MappingProfile()));
            #endregion
            #region Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options=>
            {
                //Password Validations
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
            }
                )
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // After the Identity configuration

            #region External Authentication Providers
            builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "";
                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "";
                    options.CallbackPath = "/signin-google";
                    // Optional: Request additional scopes
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"] ?? "";
                    options.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"] ?? "";
                    options.CallbackPath = "/signin-microsoft";
                    // Optional: Request additional scopes
                    options.Scope.Add("User.Read");
                });

            // Add after authentication configuration
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });
            #endregion
            #endregion
            #region DataSeeding
            builder.Services.AddScoped<IDataSeeding,DataSeeding>();
            #endregion

            #endregion
            var app = builder.Build();
            using var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            ObjectOfDataSeeding.SeedData();
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
                pattern: "{controller=Home}/{action=Landing}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
