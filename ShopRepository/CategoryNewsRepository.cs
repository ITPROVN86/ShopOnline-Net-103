using ShopBusinessLogic.Models;
using ShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public class CategoryNewsRepository : ICategoryNewsRepository
    {
        public async Task Add(CategoryNews categoryNews)
        {
            await CategoryNewsDao.Instance.Add(categoryNews);
        }

        public async Task Delete(int id)
        {
            await CategoryNewsDao.Instance.Delete(id);
        }

        public async Task<IEnumerable<CategoryNews>> GetAllCategoryNews()
        {
            return await CategoryNewsDao.Instance.GetCategoryNewsAll();
        }

        public async Task<CategoryNews> GetCategoryNewsById(int id)
        {
            return await CategoryNewsDao.Instance.GetCategoryNewsById(id);
        }

        public async Task<IEnumerable<CategoryNews>> GetCategoryNewsByName(string name)
        {
            return await CategoryNewsDao.Instance.GetCategoryNewsByName(name);
        }

        public async Task Update(CategoryNews categoryNews)
        {
            await CategoryNewsDao.Instance.Update(categoryNews);
        }
        public async Task<bool> ChangeStatus(int id)
        {
            return await CategoryNewsDao.Instance.ChangeStatus(id);
        }
    }
}
