using ShopBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRole();
        Role GetRoleById(int id);
        void Add(Role role);
        void Update(Role role);
        void Delete(int id);
        IEnumerable<Role> GetRoleByName(string name);
    }
}
