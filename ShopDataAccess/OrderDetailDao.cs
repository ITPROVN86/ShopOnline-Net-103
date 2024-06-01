using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class OrderDetailDao : SingletonBase<OrderDetailDao>
    {
        private Net103Context _context;
        public IEnumerable<OrderDetail> GetOrderDetailAll()
        {
            return _context.OrderDetails.ToList();
        }
        public IEnumerable<OrderDetail> GetOrderDetailByOrderId(int id)
        {
            return _context.OrderDetails.Where(od=>od.OrderId==id).ToList();
        }
        public void Add(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }
        public void Update(OrderDetail orderDetail)
        {
            var existingItem = _context.OrderDetails.SingleOrDefault(od=>od.OrderId== orderDetail.OrderId&&od.ProductId==orderDetail.ProductId);
            if (existingItem == null) return;
            _context.OrderDetails.Update(orderDetail);
            _context.SaveChanges();
        }
        public void Delete(int orderId, int productId)
        {
            var orderDetail = _context.OrderDetails.SingleOrDefault(od => od.OrderId == orderId && od.ProductId == productId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove((OrderDetail)orderDetail);
                _context.SaveChanges();
            }
        }
    }
}
