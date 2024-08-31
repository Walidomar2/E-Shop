﻿using E_Shop.Domain.Interfaces;
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
            var products = _unitOfWork.Product.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int id) 
        {
            ShopCart cart = new ShopCart()
            {
                Product = _unitOfWork.Product.Get(p => p.Id == id, experession: "Category"),
                Count = 1
            };
           
            return View(cart);
        }
    }
}
