using ShopBusinessLogic.Models;
using ShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public class UserRepository : IUserRepository
    {
        public async Task Add(User user)
        {
            await UserDao.Instance.Add(user);
        }

        public async Task Delete(int id)
        {
            await UserDao.Instance.Delete(id);
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await UserDao.Instance.GetUserAll();
        }

        public async Task<User> GetUserById(int id)
        {
            return await UserDao.Instance.GetUserById(id);
        }

        public IEnumerable<User> GetUserByName(string name)
        {
            return UserDao.Instance.GetUserByName(name);
        }

        public User GetUserByUserNamePass(string username, string password)
        {
            return UserDao.Instance.GetUserByUserNamePass(username, password);
        }

        public async Task Update(User user)
        {
            await UserDao.Instance.Update(user);
        }

        public async Task<bool> ChangeStatus(int id)
        {
            return await UserDao.Instance.ChangeStatus(id);
        }
    }
}
