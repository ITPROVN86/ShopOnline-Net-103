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
        public void Add(Role role)
        {
            RoleDao.Instance.Add(role);
        }

        public void Delete(int id)
        {
            RoleDao.Instance.Delete(id);
        }

        public IEnumerable<Role> GetAllRole()
        {
            return RoleDao.Instance.GetRoleAll();
        }

        public Role GetRoleById(int id)
        {
            return RoleDao.Instance.GetRoleById(id);
        }

        public IEnumerable<Role> GetRoleByName(string name)
        {
            return RoleDao.Instance.GetRoleByName(name);
        }

        public void Update(Role role)
        {
            RoleDao.Instance.Update(role);
        }
    }
}
