using ShopBusinessLogic.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class UserDao: SingletonBase<UserDao>
    {
        
        public IEnumerable<User> GetUserAll()
        {
            return _context.Users.ToList();
        }
        public User GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return null;

            return user;
        }
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void Update(User user)
        {
            var existingItem = _context.Users.Find(user.UserId);
            if (existingItem == null) return;
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User GetUserByUserNamePass(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u=>u.UserName.Equals(username) && u.Password.Equals(password));
            if (user == null) return null;

            return user;
        }

        public IEnumerable<User> GetUserByName(string name)
        {
            return _context.Users.Where(u=>u.FullName.Contains(name)).ToList();
        }
    }
}
