using E_Shop.Domain.Models;


namespace E_Shop.Domain.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> CartList { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
