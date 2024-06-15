using Microsoft.EntityFrameworkCore;
using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopDataAccess
{
    public class CategoryDao:SingletonBase<CategoryDao>
    {
        public async Task<IEnumerable<Category>> GetCategoryAll()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null) return null;
            return category;
        }
        public async Task Add(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Category category)
        {
            var existingItem = await GetCategoryById(category.CategoryId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(category);
            }
            else
            {
                _context.Categories.Add(category);
            }
            
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var category = await GetCategoryById(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetCategoryByName(string name)
        {
            var category = _context.Categories;
            return await category.Where(u => u.CategoryName.Contains(name)).ToListAsync();
        }

        public async Task<bool> ChangeStatus(int id)
        {
            var category = await GetCategoryById(id);
            category.Status = !category.Status;
            await _context.SaveChangesAsync();
            return category.Status;
        }
    }
}