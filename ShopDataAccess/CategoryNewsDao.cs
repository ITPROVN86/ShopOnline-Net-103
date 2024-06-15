using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<CategoryNews>> GetCategoryNewsAll()
        {
            return await _context.CategoryNews.ToListAsync();
        }
        public async Task<CategoryNews> GetCategoryNewsById(int id)
        {
            var categoryNews = await _context.CategoryNews.FirstOrDefaultAsync(c => c.CategoryNewsId == id);
            if (categoryNews == null) return null;

            return categoryNews;
        }
        public async Task Add(CategoryNews categoryNews)
        {
            _context.CategoryNews.Add(categoryNews);
            await _context.SaveChangesAsync();
        }
        public async Task Update(CategoryNews categoryNews)
        {
            var existingItem = await GetCategoryNewsById(categoryNews.CategoryNewsId);
            if (existingItem == null) return;
            _context.Entry(existingItem).CurrentValues.SetValues(categoryNews);
            _context.SaveChanges();
        }
        public async Task Delete(int id)
        {
            var categoryNews = await GetCategoryNewsById(id);
            if (categoryNews != null)
            {
                _context.CategoryNews.Remove(categoryNews);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CategoryNews>> GetCategoryNewsByName(string name)
        {
            var categoryNews = _context.CategoryNews;
            return await categoryNews.Where(u => u.CategoryNewsName.Contains(name)).ToListAsync();
        }

        public async Task<bool> ChangeStatus(int id)
        {
            var newsCategory = await GetCategoryNewsById(id);
            newsCategory.Status = !newsCategory.Status;
            await _context.SaveChangesAsync();
            return newsCategory.Status;
        }
    }
}
