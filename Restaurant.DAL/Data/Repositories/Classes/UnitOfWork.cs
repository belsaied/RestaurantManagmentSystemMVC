using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Data.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        #region private readonly fields
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMenuItemRepository _MenuItemRepository;
        private readonly IOrderRepository _OrderRepository;
        private readonly IOrderItemRepository _OrderItemRepository;
        private readonly ITableRepository _TableRepository;
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IIngredientRepository _IngredientRepository;
        private readonly IPaymentRepository _PaymentRepository;
        private readonly IRecipeLineRepository _RecipeLineRepository;
        private readonly AppDbContext _dbContext ;
        #endregion

        #region Constructor Injection
        public UnitOfWork(ICategoryRepository categoryRepository
           , IMenuItemRepository menuItemRepository,
           IOrderRepository orderRepository
        , IOrderItemRepository orderItemRepository,
           ITableRepository tableRepository,
           ICustomerRepository customerRepository
        , IIngredientRepository ingredientRepository,
           IPaymentRepository paymentRepository,
           IRecipeLineRepository recipeLineRepository,
           AppDbContext dbContext)
        {
            _categoryRepository = categoryRepository;

            _MenuItemRepository = menuItemRepository;

            _OrderRepository = orderRepository;

            _OrderItemRepository = orderItemRepository;

            _TableRepository = tableRepository;

            _CustomerRepository = customerRepository;

            _IngredientRepository = ingredientRepository;

            _PaymentRepository = paymentRepository;

            _RecipeLineRepository = recipeLineRepository;
            _dbContext = dbContext;

        }

        #endregion

        #region Properties
        public ICategoryRepository CategoryRepository => _categoryRepository;

        public IMenuItemRepository MenuItemRepository => _MenuItemRepository;

        public IOrderRepository OrderRepository => _OrderRepository;

        public IOrderItemRepository OrderItemRepository => _OrderItemRepository;

        public ITableRepository TableRepository => _TableRepository;

        public ICustomerRepository CustomerRepository => _CustomerRepository;

        public IIngredientRepository IngredientRepository => _IngredientRepository;

        public IPaymentRepository PaymentRepository => _PaymentRepository;

        public IRecipeLineRepository RecipeLineRepository => _RecipeLineRepository; 
        #endregion

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

    }
}
