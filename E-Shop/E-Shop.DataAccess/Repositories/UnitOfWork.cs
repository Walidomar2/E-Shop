using E_Shop.DataAccess.Data;
using E_Shop.Domain.Interfaces;


namespace E_Shop.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
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
