using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public interface INewsRepository
    {
        IEnumerable<News> GetAllNews();
        News GetNewsById(int id);
        void Add(News news);
        void Update(News news);
        void Delete(int id);
        IEnumerable<News> GetNewsByName(string name);
    }
}
