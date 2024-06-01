using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class ProductDao : SingletonBase<ProductDao>
    {
        private Net103Context _context;
        public IEnumerable<Product> GetProductAll()
        {
            return _context.Products.ToList();
        }
        public Product GetProductById(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return null;

            return product;
        }
        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void Update(Product product)
        {
            var existingItem = _context.Products.Find(product.ProductId);
            if (existingItem == null) return;
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var role = _context.Products.Find(id);
            if (role != null)
            {
                _context.Products.Remove(role);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetProductByName(string name)
        {
            return _context.Products.Where(u => u.ProductName.Contains(name)).ToList();
        }
    }
}
