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
        public IEnumerable<Category> GetCategoryAll()
        {
            return _context.Categories.AsNoTrackingWithIdentityResolution().ToList();
        }
        public Category GetCategoryById(int id)
        {
            var category = _context.Categories.AsNoTrackingWithIdentityResolution().FirstOrDefault(c => c.CategoryId == id);
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
            _context = new Net103Context();
            var existingItem = GetCategoryById(category.CategoryId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(category);
            }
            else
            {
                _context.Categories.Add(category);
            }
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var category = GetCategoryById(id);
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

        public bool ChangeStatus(int id)
        {
            var category = GetCategoryById(id);
            category.Status = !category.Status;
            _context.SaveChanges();
            return category.Status;
        }
    }
}