using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class CategoryNewsDao : SingletonBase<CategoryNewsDao>
    {
        private Net103Context _context;
        public IEnumerable<CategoryNews> GetCategoryNewsAll()
        {
            return _context.CategoryNews.ToList();
        }
        public CategoryNews GetCategoryNewsById(int id)
        {
            var categoryNews = _context.CategoryNews.Find(id);
            if (categoryNews == null) return null;

            return categoryNews;
        }
        public void Add(CategoryNews categoryNews)
        {
            _context.CategoryNews.Add(categoryNews);
            _context.SaveChanges();
        }
        public void Update(CategoryNews categoryNews)
        {
            var existingItem = _context.CategoryNews.Find(categoryNews.CategoryNewsId);
            if (existingItem == null) return;
            _context.CategoryNews.Update(categoryNews);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var categoryNews = _context.CategoryNews.Find(id);
            if (categoryNews != null)
            {
                _context.CategoryNews.Remove(categoryNews);
                _context.SaveChanges();
            }
        }

        public IEnumerable<CategoryNews> GetCategoryNewsByName(string name)
        {
            return _context.CategoryNews.Where(u => u.CategoryNewsName.Contains(name)).ToList();
        }
    }
}
