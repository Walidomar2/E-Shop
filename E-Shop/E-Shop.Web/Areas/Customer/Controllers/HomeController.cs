using E_Shop.Domain.Interfaces;
using E_Shop.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Details(int id) 
        {
            ShoppingCart cart = new ShoppingCart()
            {
                Product = _unitOfWork.Product.Get(p => p.Id == id, experession: "Category"),
                Count = 1
            };

            ViewData["HeaderTitle"] = "Your Product Details";
            return View(cart);
        }
    }
}
