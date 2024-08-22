using E_Shop.DataAccess.Data;
using E_Shop.Domain.Interfaces;
using E_Shop.Domain.Models;
using E_Shop.DataAccess.Repositories;

namespace E_Shop.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var productModel = _context.Products.FirstOrDefault(x => x.Id == product.Id);

            if (productModel != null)
            {
                productModel.Description = product.Description;
                productModel.Name = product.Name;
                productModel.Price = product.Price;
                productModel.Img = product.Img; 

            }
        }
    }
}
