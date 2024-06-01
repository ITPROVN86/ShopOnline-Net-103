using ShopBusinessLogic.Models;
using ShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void Add(OrderDetail orderDetail)
        {
            OrderDetailDao.Instance.Add(orderDetail);
        }

        public void Delete(int orderId, int productId)
        {
            OrderDetailDao.Instance.Delete(orderId, productId);
        }

        public IEnumerable<OrderDetail> GetAllOrderDetail()
        {
            return OrderDetailDao.Instance.GetOrderDetailAll();
        }

        public IEnumerable<OrderDetail> GetOrderDetailById(int id)
        {
            return OrderDetailDao.Instance.GetOrderDetailByOrderId(id);
        }

        public void Update(OrderDetail orderDetail)
        {
            OrderDetailDao.Instance.Update(orderDetail);
        }
    }
}
