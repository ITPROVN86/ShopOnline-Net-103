using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class NewsDao : SingletonBase<NewsDao>
    {
        private Net103Context _context;
        public IEnumerable<News> GetNewsAll()
        {
            return _context.News.ToList();
        }
        public News GetNewsById(int id)
        {
            var news = _context.News.Find(id);
            if (news == null) return null;

            return news;
        }
        public void Add(News news)
        {
            _context.News.Add(news);
            _context.SaveChanges();
        }
        public void Update(News news)
        {
            var existingItem = _context.News.Find(news.NewsId);
            if (existingItem == null) return;
            _context.News.Update(news);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var role = _context.News.Find(id);
            if (role != null)
            {
                _context.News.Remove(role);
                _context.SaveChanges();
            }
        }

        public IEnumerable<News> GetNewsByName(string name)
        {
            return _context.News.Where(u => u.Title.Contains(name)).ToList();
        }
    }
}
