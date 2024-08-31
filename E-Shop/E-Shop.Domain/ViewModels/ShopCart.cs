
using E_Shop.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Shop.Domain.ViewModels
{
    public class ShopCart
    {
        public Product Product { get; set; }

        [Range(1,100, ErrorMessage = "Value Must be Between 1 To 100")]
        public int Count { get; set; }
    }
}
