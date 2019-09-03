using PingYourPackage.Domain.Entitys.Core;
using System.Linq;

namespace PingYourPackage.Domain.Entitys.Extensions
{
    public static class UserRepositoryExtensions
    {
        public static User GetSingleByUserName(this IEntityRepository<User> userRepository, string userName)
        {
            return userRepository.GetAll().FirstOrDefault(u => u.Name == userName);
        }
    }
}
