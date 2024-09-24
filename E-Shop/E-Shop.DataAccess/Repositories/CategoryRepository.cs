using E_Shop.DataAccess.Data;
using E_Shop.Domain.Interfaces;
using E_Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.DataAccess.Repositories
{
    public class CategoryRepository : Repository<Category>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;


        public CategoryRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var categoryModel = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            if (categoryModel != null)
            {
                categoryModel.Description = category.Description;
                categoryModel.Name = category.Name;
                categoryModel.CreatedTime = DateTime.Now;
            }
        }
    }
}
