using E_Shop.Domain.Interfaces;
using E_Shop.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Shop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["HeaderTitle"] = "Products Home";
            var products = _unitOfWork.Product.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int productId) 
        {
            ShoppingCart cart = new ShoppingCart()
            {
                ProductId = productId,
                Product = _unitOfWork.Product.Get(p => p.Id == productId, experession: "Category"),
                Count = 1
            };

            ViewData["HeaderTitle"] = "Your Product Details";
            return View(cart);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            _unitOfWork.ShoppingCart.Add(shoppingCart); 
            _unitOfWork.Save();  

            return RedirectToAction("index");
        }
    }
}
