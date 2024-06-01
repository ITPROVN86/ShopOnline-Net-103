using ShopBusinessLogic.Models;
using ShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public class NewsRepository : INewsRepository
    {
        public void Add(News news)
        {
            NewsDao.Instance.Add(news);
        }

        public void Delete(int id)
        {
            NewsDao.Instance.Delete(id);
        }

        public IEnumerable<News> GetAllNews()
        {
            return NewsDao.Instance.GetNewsAll();
        }

        public News GetNewsById(int id)
        {
            return NewsDao.Instance.GetNewsById(id);
        }

        public IEnumerable<News> GetNewsByName(string name)
        {
            return NewsDao.Instance.GetNewsByName(name);
        }

        public void Update(News news)
        {
            NewsDao.Instance.Update(news);
        }
    }
}
