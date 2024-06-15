using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategory();
        Task<Category> GetCategoryById(int id);
        Task Add(Category category);
        Task Update(Category category);
        Task Delete(int id);
        Task<IEnumerable<Category>> GetCategoryByName(string name);
        Task<bool> ChangeStatus(int id);
    }
}
