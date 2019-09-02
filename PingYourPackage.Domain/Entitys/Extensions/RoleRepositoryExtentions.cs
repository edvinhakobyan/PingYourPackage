using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingYourPackage.Domain
{
    public static class RoleRepositoryExtentions
    {
        public static Role GetSingleByRoleNAme(this IEntityRepository<Role> roleRepository, string roleName)
        {
            return roleRepository.GetAll().FirstOrDefault(r => r.Name == roleName);
        }
    }
}
