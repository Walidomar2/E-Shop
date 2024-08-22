using E_Shop.DataAccess.Data;
using E_Shop.Domain.Interfaces;
using E_Shop.Domain.Models;
using E_Shop.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Shop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetData()
        {
            var products = _unitOfWork.Product.GetAll(experession: "Category");
            return Json(new { data = products });  
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductVM product = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().
                    Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    })
            };
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM, IFormFile file)
        {
            if (file != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string imgName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string imgPath = Path.Combine(wwwRootPath, @"Images\Products");

                // in case you update the image
                if (!string.IsNullOrEmpty(productVM.Product.Img))
                {
                    var oldImgPath =
                        Path.Combine(wwwRootPath, productVM.Product.Img.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImgPath))
                    {
                        System.IO.File.Delete(oldImgPath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(imgPath, imgName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productVM.Product.Img = @"\Images\Products\" + imgName;
            }


            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["Created"] = "Created Successfully";
                return RedirectToAction("Index");
            }
            return View(productVM.Product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var product = _unitOfWork.Product.Get(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                TempData["Edited"] = "Edited Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var product = _unitOfWork.Product.Get(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            var product = _unitOfWork.Product.Get(x => x.Id == id);

            if (product == null)
                return NotFound();

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["Deleted"] = "Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
