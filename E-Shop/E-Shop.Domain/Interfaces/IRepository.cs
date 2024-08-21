
using System.Linq.Expressions;

namespace E_Shop.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? perdicate = null, string? experession = null);
        T Get(Expression<Func<T, bool>>? perdicate = null, string? experession = null);
        void Remove(T entity);   
        void Add(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
