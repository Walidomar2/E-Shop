using Microsoft.AspNetCore.Identity;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;

=======
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> cc2814a7a513e0b822e3eabcae5497ceb0b7a6ab

namespace E_Shop.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
