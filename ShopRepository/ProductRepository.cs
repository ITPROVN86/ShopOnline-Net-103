using ShopBusinessLogic.Models;
using ShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public class ProductRepository : IProductRepository
    {
        public async Task Add(Product product)
        {
            await ProductDao.Instance.Add(product);
        }

        public async Task Delete(int id)
        {
            await ProductDao.Instance.Delete(id);
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await ProductDao.Instance.GetProductAll();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await ProductDao.Instance.GetProductById(id);
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await ProductDao.Instance.GetProductByName(name);
        }

        public async Task Update(Product product)
        {
            await ProductDao.Instance.Update(product);
        }

        public async Task<bool> ChangeStatus(int id)
        {
            return await ProductDao.Instance.ChangeStatus(id);
        }
    }
}
