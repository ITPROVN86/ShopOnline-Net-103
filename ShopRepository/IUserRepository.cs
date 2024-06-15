using ShopBusinessLogic.Models;
using ShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(int id);
        Task Add(User user);
        Task Update(User user);
        Task Delete(int id);
        User GetUserByUserNamePass(string username, string password);
        IEnumerable<User> GetUserByName(string name);
        Task<bool> ChangeStatus(int id);
    }
}
