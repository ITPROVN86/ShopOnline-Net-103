using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrder();
        Order GetOrderById(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
    }
}
