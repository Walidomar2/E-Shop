using E_Shop.DataAccess.Repositories;
using E_Shop.Domain.Interfaces;
using E_Shop.Domain.ViewModels;
using E_Shop.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult StartProccess()
		{
            _unitOfWork.OrderHeader.UpdateOrderStatus(OrderVM.OrderHeader.Id, SD.Proccessing, null);
			_unitOfWork.Save();

			TempData["Edited"] = "Status Changed Successfully";

			return RedirectToAction("Details", "Order", new { orderid = OrderVM.OrderHeader.Id });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult StartShip()
		{
            var orderFromDB = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderFromDB.Carrier = OrderVM.OrderHeader.Carrier;
            orderFromDB.OrderStatus = SD.Shipped;
            orderFromDB.ShippingDate = DateTime.Now;
            _unitOfWork.OrderHeader.Update(orderFromDB);
            _unitOfWork.Save();

			TempData["Edited"] = "Order Shipped Successfully";

			return RedirectToAction("Details", "Order", new { orderid = OrderVM.OrderHeader.Id });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CancelOrder()
		{
			var orderfromdb = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
			if (orderfromdb.PaymentStatus == SD.Approve)
			{
				var option = new RefundCreateOptions
				{
					Reason = RefundReasons.RequestedByCustomer,
					PaymentIntent = orderfromdb.PaymentIntentId
				};

				var service = new RefundService();
				Refund refund = service.Create(option);

				_unitOfWork.OrderHeader.UpdateOrderStatus(orderfromdb.Id, SD.Cancelled, SD.Refund);
			}
			else
			{
				_unitOfWork.OrderHeader.UpdateOrderStatus(orderfromdb.Id, SD.Cancelled, SD.Cancelled);
			}
			_unitOfWork.Save();

			TempData["Update"] = "Order has Cancelled Successfully";
			return RedirectToAction("Details", "Order", new { orderid = OrderVM.OrderHeader.Id });
		}

	}
}
