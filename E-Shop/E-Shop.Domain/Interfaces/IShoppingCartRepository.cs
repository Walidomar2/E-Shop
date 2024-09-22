
using E_Shop.Domain.Models;

namespace E_Shop.Domain.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
       int IncreaseCount(ShoppingCart shoppingCart, int count);
       int DecreaseCount(ShoppingCart shoppingCart, int count);
    }
}
