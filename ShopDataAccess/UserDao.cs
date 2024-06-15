using Microsoft.EntityFrameworkCore;
using ShopBusinessLogic.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class UserDao : SingletonBase<UserDao>
    {
        public async Task<IEnumerable<User>> GetUserAll()
        {
            return await _context.Users.Include(r => r.Role).ToListAsync();
        }
        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;
            return user;
        }
        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task Update(User user)
        {
            //_context = new Net103Context();
            var existingItem = await GetUserById(user.UserId);
            var password = user.Password;
            if (string.IsNullOrEmpty(password))
            {
                user.Password = existingItem.Password;
            }
            if (existingItem == null) return;
            //_context.Entry<User>(user).State = EntityState.Modified;
            _context.Entry(existingItem).CurrentValues.SetValues(user);
            //_context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var user = await GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public User GetUserByUserNamePass(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName.Equals(username) && u.Password.Equals(password));
            if (user == null) return null;

            return user;
        }

        public IEnumerable<User> GetUserByName(string name)
        {
            return _context.Users.Where(u => u.FullName.Contains(name)).ToList();
        }

        public async Task<bool> ChangeStatus(int id)
        {
            var category =await GetUserById(id);
            category.Status = !category.Status;
            _context.SaveChanges();
            return category.Status;
        }
    }
}
