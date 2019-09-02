using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingYourPackage.Domain
{
    public static class UserRepositoryExtensions
    {
        public static User GetSingleByUserName(this IEntityRepository<User> userRepository, string userName)
        {
            return userRepository.GetAll().FirstOrDefault(u => u.Name == userName);
        }
    }
}
