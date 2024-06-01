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
        public void Add(User user)
        {
            UserDao.Instance.Add(user);
        }

        public void Delete(int id)
        {
            UserDao.Instance.Delete(id);
        }

        public IEnumerable<User> GetAllUser()
        {
            return UserDao.Instance.GetUserAll();
        }

        public User GetUserById(int id)
        {
            return UserDao.Instance.GetUserById(id);
        }

        public IEnumerable<User> GetUserByName(string name)
        {
            return UserDao.Instance.GetUserByName(name);
        }

        public User GetUserByUserNamePass(string username, string password)
        {
            return UserDao.Instance.GetUserByUserNamePass(username, password);
        }

        public void Update(User user)
        {
            UserDao.Instance.Update(user);
        }
    }
}
