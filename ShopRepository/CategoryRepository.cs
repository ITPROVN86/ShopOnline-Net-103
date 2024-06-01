using ShopBusinessLogic.Models;
using ShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        public void Add(Category category)
        {
            CategoryDao.Instance.Add(category);
        }

        public void Delete(int id)
        {
            CategoryDao.Instance.Delete(id);
        }

        public IEnumerable<Category> GetAllCategory()
        {
            return CategoryDao.Instance.GetCategoryAll();
        }

        public Category GetCategoryById(int id)
        {
            return CategoryDao.Instance.GetCategoryById(id);
        }

        public IEnumerable<Category> GetCategoryByName(string name)
        {
            return CategoryDao.Instance.GetCategoryByName(name);
        }

        public void Update(Category category)
        {
            CategoryDao.Instance.Update(category);
        }
        public bool ChangeStatus(int id)
        {
            return CategoryDao.Instance.ChangeStatus(id);
        }
    }
}
