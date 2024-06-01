using ShopBusinessLogic.Models;
using ShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public class OrderRepository : IOrderRepository
    {
        public void Add(Order order)
        {
            OrderDao.Instance.Add(order);
        }

        public void Delete(int id)
        {
            OrderDao.Instance.Delete(id);
        }

        public IEnumerable<Order> GetAllOrder()
        {
            return OrderDao.Instance.GetOrderAll();
        }

        public Order GetOrderById(int id)
        {
            return OrderDao.Instance.GetOrderById(id);
        }

        public void Update(Order order)
        {
            OrderDao.Instance.Update(order);
        }
    }
}
