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
        public async Task Add(Category category)
        {
            await CategoryDao.Instance.Add(category);
        }

        public async Task Delete(int id)
        {
            await CategoryDao.Instance.Delete(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await CategoryDao.Instance.GetCategoryAll();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await CategoryDao.Instance.GetCategoryById(id);
        }

        public async Task<IEnumerable<Category>> GetCategoryByName(string name)
        {
            return await CategoryDao.Instance.GetCategoryByName(name);
        }

        public async Task Update(Category category)
        {
            await CategoryDao.Instance.Update(category);
        }
        public async Task<bool> ChangeStatus(int id)
        {
            return await CategoryDao.Instance.ChangeStatus(id);
        }
    }
}
