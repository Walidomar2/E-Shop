using E_Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);

    }
}
