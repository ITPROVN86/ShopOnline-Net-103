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
        Task<IEnumerable<Role>> GetAllRole();
        Task<Role> GetRoleById(int id);
        Task Add(Role role);
        Task Update(Role role);
        Task Delete(int id);
        Task<IEnumerable<Role>> GetRoleByName(string name);
    }
}
