using E_Shop.DataAccess.Data;
using E_Shop.Domain.Interfaces;
using E_Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.DataAccess.Repositories
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderHeader orderHeader)
        {
            _context.OrderHeaders.Update(orderHeader);
        }

        public void UpdateOrderStatus(int id, string OrderStatus, string PaymentStatus)
        {
            var orderFromDB = _context.OrderHeaders.FirstOrDefault(x => x.Id == id);

            if (orderFromDB != null)
            {
                orderFromDB.OrderStatus = OrderStatus;
                orderFromDB.PaymentDate = DateTime.Now;
                if (PaymentStatus != null)
                {
                    orderFromDB.PaymentStatus = PaymentStatus;
                }
            }
        }
    }
}
