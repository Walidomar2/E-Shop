using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderHeaderRepository Category { get; }
        IProductRepository Product { get; }
        IShoppingCartRepository ShoppingCart { get;}
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }
        int Save();
    }
}
