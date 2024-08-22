using E_Shop.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Shop.Domain.ViewModels
{
    public class ProductVM
    {
        public Product Product{ get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList{ get; set; }
    }
}
