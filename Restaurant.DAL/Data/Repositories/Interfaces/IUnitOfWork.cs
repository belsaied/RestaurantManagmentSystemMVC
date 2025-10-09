using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IMenuItemRepository MenuItemRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderItemRepository OrderItemRepository { get; }
        public ITableRepository TableRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IIngredientRepository IngredientRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IRecipeLineRepository RecipeLineRepository { get; }

        public int SaveChanges();

    }
}
