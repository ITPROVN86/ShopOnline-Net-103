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
        public void Add(Product product)
        {
            ProductDao.Instance.Add(product);
        }

        public void Delete(int id)
        {
            ProductDao.Instance.Delete(id);
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return ProductDao.Instance.GetProductAll();
        }

        public Product GetProductById(int id)
        {
            return ProductDao.Instance.GetProductById(id);
        }

        public IEnumerable<Product> GetProductByName(string name)
        {
            return ProductDao.Instance.GetProductByName(name);
        }

        public void Update(Product product)
        {
            ProductDao.Instance.Update(product);
        }
    }
}
