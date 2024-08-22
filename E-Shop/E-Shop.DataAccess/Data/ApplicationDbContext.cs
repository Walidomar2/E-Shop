using E_Shop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
    }
}
