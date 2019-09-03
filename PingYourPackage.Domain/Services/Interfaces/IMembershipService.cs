using PingYourPackage.Domain.Entitys;
using PingYourPackage.Domain.Entitys.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingYourPackage.Domain.Services.Interfaces
{
    public interface IMembershipService
    {
        ValidUserContext ValidUser(string userName, string password);
        OperationResult<UserWithRoles> CreateUser(string userName, string email, string password);
        OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string role);
        OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string[] roles);
        void UpdateUser(User user, string userName, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        bool AddToRole(Guid userKey, string role);
        bool AddToRole(string userName, string role);
        bool RemoveFromRole(string userName, string rele);
        IEnumerable<Role> GetRoles();
        Role GetRole(Guid key);
        Role GetRole(string name);
        PaginatedList<UserWithRoles> GetUsers(int pageIndex, int pageSize);
        UserWithRoles GetUserWithRoles(Guid key);
        UserWithRoles GetUserWithRoles(string name);
    }
}
