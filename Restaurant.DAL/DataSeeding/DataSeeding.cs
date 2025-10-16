using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Restaurant.DAL.DataSeeding
{
    public class DataSeeding(AppDbContext _dbContext) : IDataSeeding
    {
        public void SeedData()
        {

            //if (_dbContext.Database.GetPendingMigrations().Any())
            //{
            //    _dbContext.Database.Migrate();
            //}
            //if (!_dbContext.Customers.Any())
            //{
            //    var CustomersData = File.ReadAllText("C:\\Users\\HPi7\\Source\\Repos\\RestaurantSystem\\Restaurant.DAL\\DataSeeding\\JsonFiles\\Customers.json");
            //    var Customers = JsonSerializer.Deserialize<List<Customer>>(CustomersData);
            //    if (Customers is not null && Customers.Any())
            //    {
            //        _dbContext.AddRange(Customers);
            //    }
            //}
            //if (!_dbContext.Tables.Any())
            //{
            //    var TablesData = File.ReadAllText("C:\\Users\\HPi7\\Source\\Repos\\RestaurantSystem\\Restaurant.DAL\\DataSeeding\\JsonFiles\\Tables.json");
            //    var Tables = JsonSerializer.Deserialize<List<Table>>(TablesData);
            //    if (Tables is not null && Tables.Any())
            //    {
            //        _dbContext.AddRange(Tables);
            //    }
            //}
            //if (!_dbContext.Orders.Any())
            //{
            //    var OrdersData = File.ReadAllText("C:\\Users\\HPi7\\Source\\Repos\\RestaurantSystem\\Restaurant.DAL\\DataSeeding\\JsonFiles\\Orders.json");
            //    var Orders = JsonSerializer.Deserialize<List<Order>>(OrdersData);
            //    if (Orders is not null && Orders.Any())
            //    {
            //        _dbContext.AddRange(Orders);
            //    }
            //}
            //if (!_dbContext.Categories.Any())
            //{
            //    var CategoriesData = File.ReadAllText("C:\\Users\\HPi7\\Source\\Repos\\RestaurantSystem\\Restaurant.DAL\\DataSeeding\\JsonFiles\\Categories.json");
            //    var Categories = JsonSerializer.Deserialize<List<Category>>(CategoriesData);
            //    if (Categories is not null && Categories.Any())
            //    {
            //        _dbContext.AddRange(Categories);
            //    }
            //}
            //if (!_dbContext.MenuItems.Any())
            //{
            //    var MenuItemsData = File.ReadAllText("C:\\Users\\HPi7\\Source\\Repos\\RestaurantSystem\\Restaurant.DAL\\DataSeeding\\JsonFiles\\MenuItems.json");
            //    var MenuItems = JsonSerializer.Deserialize<List<MenuItem>>(MenuItemsData);
            //    if (MenuItems is not null && MenuItems.Any())
            //    {
            //        _dbContext.AddRange(MenuItems);
            //    }
            //}
            //if (!_dbContext.Payments.Any())
            //{
            //    var PaymentsData = File.ReadAllText("C:\\Users\\HPi7\\Source\\Repos\\RestaurantSystem\\Restaurant.DAL\\DataSeeding\\JsonFiles\\Payment.json");
            //    var Payments = JsonSerializer.Deserialize<List<Payment>>(PaymentsData);
            //    if (Payments is not null && Payments.Any())
            //    {
            //        _dbContext.AddRange(Payments);
            //    }
            //}
            //if (!_dbContext.OrderItems.Any())
            //{
            //    var OrderItemsData = File.ReadAllText("C:\\Users\\HPi7\\Source\\Repos\\RestaurantSystem\\Restaurant.DAL\\DataSeeding\\JsonFiles\\OrderItems.json");
            //    var OrderItems = JsonSerializer.Deserialize<List<OrderItems>>(OrderItemsData);
            //    if (OrderItems is not null && OrderItems.Any())
            //    {
            //        _dbContext.AddRange(OrderItems);
            //    }
            //}
            //if (!_dbContext.Ingredients.Any())
            //{
            //    var IngredientsData = File.ReadAllText("C:\\Users\\HPi7\\Source\\Repos\\RestaurantSystem\\Restaurant.DAL\\DataSeeding\\JsonFiles\\Ingredients.json");
            //    var Ingredients = JsonSerializer.Deserialize<List<Ingredient>>(IngredientsData);
            //    if (Ingredients is not null && Ingredients.Any())
            //    {
            //        _dbContext.AddRange(Ingredients);
            //    }
            //}
            //if (!_dbContext.RecipeLines.Any())
            //{
            //    var RecipeLinesData = File.ReadAllText("C:\\Users\\HPi7\\Source\\Repos\\RestaurantSystem\\Restaurant.DAL\\DataSeeding\\JsonFiles\\RecipeLines.json");
            //    var RecipeLines = JsonSerializer.Deserialize<List<RecipeLine>>(RecipeLinesData);
            //    if (RecipeLines is not null && RecipeLines.Any())
            //    {
            //        _dbContext.AddRange(RecipeLines);
            //    }
            //}

            //_dbContext.SaveChanges();



        }
    }
}
