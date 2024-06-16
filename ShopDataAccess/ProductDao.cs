using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Product>> GetProductAll()
        {
            return await _context.Products.Include(p=>p.UserPostNavigation).Include(p=>p.Category).ToListAsync();
        }
        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            return product;
        }
        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Product product)
        {
            var existingItem = await GetProductById(product.ProductId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(product);
                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var role = await GetProductById(id);
            if (role != null)
            {
                _context.Products.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _context.Products.Where(u => u.ProductName.Contains(name)).Include(p => p.UserPostNavigation).Include(p => p.Category).ToListAsync();
        }

        public async Task<bool> ChangeStatus(int id)
        {
            var product = await GetProductById(id);
            product.Status = !product.Status;
            _context.SaveChanges();
            return Convert.ToBoolean(product.Status);
        }
    }
}
