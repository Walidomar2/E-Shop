using E_Shop.Domain.Interfaces;
using E_Shop.Domain.ViewModels;
using E_Shop.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Admin)]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public OrderVM OrderVM { get; set; }

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetData()
        {
            IEnumerable<OrderHeader> orderHeaders;
            orderHeaders = _unitOfWork.OrderHeader.GetAll(experession:"ApplicationUser");
            return Json(new {data = orderHeaders});
        }

        public IActionResult Details(int orderid)
        {
            OrderVM orderVM = new OrderVM()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id==orderid, experession:"ApplicationUser"),
                OrderDetails = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId==orderid,experession:"Product")
            };

            return View(orderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderDetails() 
        {
            var orderFromDB = _unitOfWork.OrderHeader.Get(o => o.Id == OrderVM.OrderHeader.Id);
            orderFromDB.Name = OrderVM.OrderHeader.Name;    
            orderFromDB.Address = OrderVM.OrderHeader.Address;
            orderFromDB.Phone = OrderVM.OrderHeader.Phone;  

            if(OrderVM.OrderHeader.Carrier != null)
            {
                orderFromDB.Carrier = OrderVM.OrderHeader.Carrier;  
            }

            _unitOfWork.OrderHeader.Update(orderFromDB);
            _unitOfWork.Save();
			TempData["Edited"] = "Edited Successfully";

			return RedirectToAction("Details","Order",new {orderid = orderFromDB.Id});
        }  
    }
}
