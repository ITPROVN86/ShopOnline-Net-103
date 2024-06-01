using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public interface ICategoryNewsRepository
    {
        IEnumerable<CategoryNews> GetAllCategoryNews();
        CategoryNews GetCategoryNewsById(int id);
        void Add(CategoryNews categoryNews);
        void Update(CategoryNews categoryNews);
        void Delete(int id);
        IEnumerable<CategoryNews> GetCategoryNewsByName(string name);
    }
}
