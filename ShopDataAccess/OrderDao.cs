using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class OrderDao:SingletonBase<OrderDao>
    {
        private Net103Context _context;
        public IEnumerable<Order> GetOrderAll()
        {
            return _context.Orders.ToList();
        }
        public Order GetOrderById(int id)
        {
            var role = _context.Orders.Find(id);
            if (role == null) return null;

            return role;
        }
        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void Update(Order order)
        {
            var existingItem = _context.Orders.Find(order.OrderId);
            if (existingItem == null) return;
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
