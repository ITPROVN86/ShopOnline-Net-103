using ShopBusinessLogic.Models;
using ShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public interface ICategoryNewsRepository
    {
        Task<IEnumerable<CategoryNews>> GetAllCategoryNews();
        Task<CategoryNews> GetCategoryNewsById(int id);
        Task Add(CategoryNews categoryNews);
        Task Update(CategoryNews categoryNews);
        Task Delete(int id);
        Task<IEnumerable<CategoryNews>> GetCategoryNewsByName(string name);

        Task<bool> ChangeStatus(int id);
    }
}
