using E_Shop.Domain.Models;
using System;


namespace E_Shop.Domain.Interfaces
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);   
        void UpdateOrderStatus(int id, string OrderStatus,string PaymentStatus);
    }
}
