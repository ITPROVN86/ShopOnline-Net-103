using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Role>> GetRoleAll()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<Role> GetRoleById(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(c => c.RoleId == id);
            if (role == null) return null;

            return role;
        }
        public async Task Add(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Role role)
        {
            var existingItem = await GetRoleById(role.RoleId);
            if (existingItem == null) return;
            _context.Entry(existingItem).CurrentValues.SetValues(role);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var role = await GetRoleById(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Role>> GetRoleByName(string name)
        {
            return await _context.Roles.Where(u => u.RoleName.Contains(name)).ToListAsync();
        }
    }
}
