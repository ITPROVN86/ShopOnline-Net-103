using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class RoleDao:SingletonBase<RoleDao>
    {
        private Net103Context _context;
        public IEnumerable<Role> GetRoleAll()
        {
            return _context.Roles.ToList();
        }
        public Role GetRoleById(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null) return null;

            return role;
        }
        public void Add(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }
        public void Update(Role role)
        {
            var existingItem = _context.Roles.Find(role.RoleId);
            if (existingItem == null) return;
            _context.Roles.Update(role);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Role> GetRoleByName(string name)
        {
            return _context.Roles.Where(u => u.RoleName.Contains(name)).ToList();
        }
    }
}
