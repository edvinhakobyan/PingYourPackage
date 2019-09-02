using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PingYourPackage.Domain.Services
{
    class MembershipService : IMembershipService
    {
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<Role> _roleRepository;
        private readonly IEntityRepository<UserInRole> _userInRoleRepository;
        private readonly ICryptoService _cryptoService;

        public MembershipService(IEntityRepository<User> userRepository, 
                                 IEntityRepository<Role> roleRepository, 
                                 IEntityRepository<UserInRole> userInRoleRepository, 
                                 ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userInRoleRepository = userInRoleRepository;
            _cryptoService = cryptoService;
        }

        public bool AddToRole(Guid userKey, string role)
        {
            throw new NotImplementedException();
        }

        public bool AddToRole(string userName, string role)
        {
            throw new NotImplementedException();
        }

        public bool ChengePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public OperationResult<UserWithRoles> CreateUser(string userName, string email, string password)
        {
            throw new NotImplementedException();
        }

        public OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string role)
        {
            throw new NotImplementedException();
        }

        public OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string[] roles)
        {
            throw new NotImplementedException();
        }

        public Role GetRole(Guid key)
        {
            throw new NotImplementedException();
        }

        public Role GetRole(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRols()
        {
            throw new NotImplementedException();
        }

        public UserWithRoles GetUser(Guid key)
        {
            throw new NotImplementedException();
        }

        public UserWithRoles GetUser(string name)
        {
            throw new NotImplementedException();
        }

        public PaginatedList<UserWithRoles> GetUsers(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFromRole(string userName, string rele)
        {
            throw new NotImplementedException();
        }

        public UserWithRoles UpdateUser(User user, string userName, string email)
        {
            throw new NotImplementedException();
        }

        public ValidUserContext ValidUser(string userName, string password)
        {
            var validUserContext = new ValidUserContext();
            var user = _userRepository.GetSingleByUserName(userName);
            if(user != null && isUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.Key);
                validUserContext.User = new UserWithRoles()
                {
                    User = user,
                    Roles = userRoles
                };

                var identity = new GenericIdentity(user.Name);
                validUserContext.Principal = new GenericPrincipal(identity, userRoles.Select(t => t.Name).ToArray());
            }
            return validUserContext;
        }

        private bool isUserValid(User user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsLocked;
            }
            return false;
        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(_cryptoService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private IEnumerable<Role> GetUserRoles(Guid userKey)
        {
            var userInRoles = _userInRoleRepository.FindBy(x => x.UserKey == userKey).ToList();
            if (userInRoles != null && userInRoles.Count > 0)
            {
                var userRoleKeys = userInRoles.Select(x => x.RoleKey).ToArray();
                var userRoles = _roleRepository.FindBy(x => userRoleKeys.Contains(x.Key));
                return userRoles;
            }
            return Enumerable.Empty<Role>();
        }
    }

}
