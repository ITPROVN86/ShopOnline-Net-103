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
        public void Add(CategoryNews categoryNews)
        {
            CategoryNewsDao.Instance.Add(categoryNews);
        }

        public void Delete(int id)
        {
            CategoryNewsDao.Instance.Delete(id);
        }

        public IEnumerable<CategoryNews> GetAllCategoryNews()
        {
            return CategoryNewsDao.Instance.GetCategoryNewsAll();
        }

        public CategoryNews GetCategoryNewsById(int id)
        {
            return CategoryNewsDao.Instance.GetCategoryNewsById(id);
        }

        public IEnumerable<CategoryNews> GetCategoryNewsByName(string name)
        {
            return CategoryNewsDao.Instance.GetCategoryNewsByName(name);
        }

        public void Update(CategoryNews categoryNews)
        {
            CategoryNewsDao.Instance.Update(categoryNews);
        }
    }
}
