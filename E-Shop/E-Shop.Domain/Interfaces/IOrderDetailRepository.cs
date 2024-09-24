using E_Shop.Domain.Models;
using System;


namespace E_Shop.Domain.Interfaces
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail orderDetail);   
        
    }
}
