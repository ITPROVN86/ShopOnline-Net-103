using ShopBusinessLogic.Models;
using ShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public class RoleRepository : IRoleRepository
    {
        public async Task Add(Role role)
        {
            await RoleDao.Instance.Add(role);
        }

        public async Task Delete(int id)
        {
            await RoleDao.Instance.Delete(id);
        }

        public async Task<IEnumerable<Role>> GetAllRole()
        {
            return await RoleDao.Instance.GetRoleAll();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await RoleDao.Instance.GetRoleById(id);
        }

        public async Task<IEnumerable<Role>> GetRoleByName(string name)
        {
            return await RoleDao.Instance.GetRoleByName(name);
        }

        public async Task Update(Role role)
        {
            await RoleDao.Instance.Update(role);
        }
    }
}
