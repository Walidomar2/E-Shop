
using E_Shop.DataAccess.Data;
using E_Shop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Shop.DataAccess.Repositories 
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public void Add(T entity)
        {
            _dbSet.AddAsync(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? perdicate = null, string? experession = null)
        {
            IQueryable<T> query = _dbSet;

            if (perdicate != null)
            { 
                query = query.Where(perdicate);
            }

            if(experession != null)
            {
                foreach(var exp in experession.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(exp);
                }
            }

            return query.ToList();
        }

        public T Get(Expression<Func<T, bool>>? perdicate = null, string? experession = null)
        {

            IQueryable<T> query = _dbSet;

            if (perdicate != null)
            {
                query = query.Where(perdicate);
            }

            if (experession != null)
            {
                foreach (var exp in experession.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(exp);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
