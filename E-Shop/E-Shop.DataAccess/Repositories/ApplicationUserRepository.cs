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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        
        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
