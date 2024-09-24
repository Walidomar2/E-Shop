using E_Shop.Domain.Interfaces;
using E_Shop.Domain.Models;
using E_Shop.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Shop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ViewData["HeaderTitle"] = "Your Cart";

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartVM()
            {
                CartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, experession: "Product")
            };

            foreach (var cart in ShoppingCartVM.CartList)
            { 
                ShoppingCartVM.TotalCarts += (cart.Count * cart.Product.Price);
            }
            return View(ShoppingCartVM);
        }

        public IActionResult Plus(int cartid)
        {
            var shoppingcart = _unitOfWork.ShoppingCart.Get(x => x.Id == cartid);

            _unitOfWork.ShoppingCart.IncreaseCount(shoppingcart, 1);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Minus(int cartid)
        {
            var shoppingcart = _unitOfWork.ShoppingCart.Get(x => x.Id == cartid);

            if (shoppingcart.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(shoppingcart);
                _unitOfWork.Save();
                return RedirectToAction("Index","Home");
            } 
            else
            {
                _unitOfWork.ShoppingCart.DecreaseCount(shoppingcart, 1);
            }

            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int cartid)
        {
            var shoppingcart = _unitOfWork.ShoppingCart.Get(x => x.Id == cartid);
            _unitOfWork.ShoppingCart.Remove(shoppingcart);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        } 
    }
}
