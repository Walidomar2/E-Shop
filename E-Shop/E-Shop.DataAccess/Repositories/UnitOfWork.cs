using E_Shop.DataAccess.Data;
using E_Shop.Domain.Interfaces;


namespace E_Shop.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product {  get; private set; }
        public IShoppingCartRepository ShoppingCart {  get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
            ShoppingCart = new ShoppingCartRepository(context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
           return _context.SaveChanges();
        }
    }
}
