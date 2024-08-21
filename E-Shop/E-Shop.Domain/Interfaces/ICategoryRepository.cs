
using E_Shop.Domain.Models;

namespace E_Shop.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category); 
    }
}
