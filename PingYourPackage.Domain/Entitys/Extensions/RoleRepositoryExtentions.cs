using PingYourPackage.Domain.Entitys.Core;
using System.Linq;

namespace PingYourPackage.Domain.Entitys.Extensions
{
    public static class RoleRepositoryExtentions
    {
        public static Role GetSingleByRoleName(this IEntityRepository<Role> roleRepository, string roleName)
        {
            return roleRepository.GetAll().FirstOrDefault(r => r.Name == roleName);
        }
    }
}
