using Restaurant.DAL.Data.Contexts;
using Restaurant.DAL.Data.Repositories.Interfaces;

namespace Restaurant.DAL.Data.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        #region private readonly fields
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IMenuItemRepository> _MenuItemRepository;
        private readonly Lazy<IOrderRepository> _OrderRepository;
        private readonly Lazy<IOrderItemRepository> _OrderItemRepository;
        private readonly Lazy<ITableRepository> _TableRepository;
        private readonly Lazy<ICustomerRepository> _CustomerRepository;
        private readonly Lazy<IIngredientRepository> _IngredientRepository;
        private readonly Lazy<IPaymentRepository> _PaymentRepository;
        private readonly Lazy<IRecipeLineRepository> _RecipeLineRepository;
        private readonly AppDbContext _dbContext;
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
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_dbContext));

            _MenuItemRepository = new Lazy<IMenuItemRepository>(() => new MenuItemRepository(_dbContext));

            _OrderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(_dbContext));

            _OrderItemRepository = new Lazy<IOrderItemRepository>(() => new OrderItemRepository(_dbContext));

            _TableRepository = new Lazy<ITableRepository>(() => new TableRepository(_dbContext));

            _CustomerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(_dbContext));

            _IngredientRepository = new Lazy<IIngredientRepository>(() => new IngredientRepository(_dbContext));

            _PaymentRepository = new Lazy<IPaymentRepository>(() => new PaymentRepository(_dbContext));

            _RecipeLineRepository = new Lazy<IRecipeLineRepository>(() => new RecipeLineRepository(_dbContext));
            _dbContext = dbContext;

        }

        #endregion

        #region Properties
        public ICategoryRepository CategoryRepository => _categoryRepository.Value;

        public IMenuItemRepository MenuItemRepository => _MenuItemRepository.Value;

        public IOrderRepository OrderRepository => _OrderRepository.Value;

        public IOrderItemRepository OrderItemRepository => _OrderItemRepository.Value;

        public ITableRepository TableRepository => _TableRepository.Value;

        public ICustomerRepository CustomerRepository => _CustomerRepository.Value;

        public IIngredientRepository IngredientRepository => _IngredientRepository.Value;

        public IPaymentRepository PaymentRepository => _PaymentRepository.Value;

        public IRecipeLineRepository RecipeLineRepository => _RecipeLineRepository.Value;
        #endregion

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

    }
}
