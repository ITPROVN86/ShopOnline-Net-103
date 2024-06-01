using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class CategoryDao:SingletonBase<CategoryDao>
    {
        private Net103Context _context;
        public IEnumerable<Category> GetCategoryAll()
        {
            return _context.Categories.ToList();
        }
        public Category GetCategoryById(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return null;

            return category;
        }
        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void Update(Category category)
        {
            var existingItem = _context.Categories.Find(category.CategoryId);
            if (existingItem == null) return;
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Category> GetCategoryByName(string name)
        {
            return _context.Categories.Where(u => u.CategoryName.Contains(name)).ToList();
        }
    }
}
