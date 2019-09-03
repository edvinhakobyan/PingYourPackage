using PingYourPackage.Domain.Entitys;
using PingYourPackage.Domain.Entitys.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using PingYourPackage.Domain.Entitys.Extensions;
using PingYourPackage.Domain.Services.Interfaces;

namespace PingYourPackage.Domain.Services
{
    public class MembershipService : IMembershipService
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

        public bool AddToRole(Guid userKey, string roleName)
        {
            var user = _userRepository.GetSingle(userKey);
            if(user != null)
            {
                addUserToRole(user, roleName);
                return true;
            }
            return false;
        }

        public bool AddToRole(string userName, string roleName)
        {
            var user = _userRepository.GetSingleByUserName(userName);
            if(user != null)
            {
                addUserToRole(user, roleName);
                return true;
            }
            return true;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var user = _userRepository.GetSingleByUserName(userName);
            var isPassworTrue = _cryptoService.EncryptPassword(oldPassword, user.Salt) == user.HashedPassword;

            if (user != null && isPassworTrue)
            {
                user.HashedPassword = _cryptoService.EncryptPassword(newPassword, user.Salt);
                _userRepository.Edit(user);
                _userRepository.Save();
                return true;
            }
            return true;
        }

        public OperationResult<UserWithRoles> CreateUser(string userName, string email, string password)
        {
            return CreateUser(userName, email, password, roles: null);
        }

        public OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string role)
        {
            return CreateUser(userName, email, password, roles: new[] { role });
        }

        public OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string[] roles)
        {
            var existingUser = _userRepository.GetAll().Any(x => x.Name == userName);
            if (existingUser)
            {
                return new OperationResult<UserWithRoles>(false);
            }

            var passwordSalt = _cryptoService.GenerateSalt();

            var user = new User()
            {
                Name = userName,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = _cryptoService.EncryptPassword(password, passwordSalt),
                CreatedOn = DateTime.Now
            };

            _userRepository.Add(user);
            _userRepository.Save();

            if (roles != null || roles.Length > 0)
            {
                foreach (var roleName in roles)
                {
                    addUserToRole(user, roleName);
                }
            }

            return new OperationResult<UserWithRoles>(true)
            {
                Entity = GetUserWithRoles(user)
            };
        }

        private UserWithRoles GetUserWithRoles(User user)
        {
            if (user != null)
            {
                var userRoles = GetUserRoles(user.Key);
                return new UserWithRoles()
                {
                    User = user,
                    Roles = userRoles
                };
            }
            return null;
        }

        public UserWithRoles GetUserWithRoles(Guid userKey)
        {
            var user = _userRepository.GetSingle(userKey);
            return GetUserWithRoles(user);
        }

        public UserWithRoles GetUserWithRoles(string userName)
        {
            var user = _userRepository.GetSingleByUserName(userName);
            return GetUserWithRoles(user);
        }

        private void addUserToRole(User user, string roleName)
        {
            var role = _roleRepository.GetSingleByRoleName(roleName);
            if (role == null)
            {
                role = new Role() { Name = roleName };
                _roleRepository.Add(role);
                _roleRepository.Save();
            }

            var userInRole = new UserInRole()
            {
                UserKey = user.Key,
                RoleKey = role.Key
            };
            _userInRoleRepository.Add(userInRole);
            _userInRoleRepository.Save();
        }

        public Role GetRole(Guid roleKey) 
        {
            return _roleRepository.GetSingle(roleKey);
        }

        public Role GetRole(string rolName)
        {
            return _roleRepository.GetSingleByRoleName(rolName);
        }

        public IEnumerable<Role> GetRoles()
        {
            return _roleRepository.GetAll();
        }

        public PaginatedList<UserWithRoles> GetUsers(int pageIndex, int pageSize)
        {
            var users = _userRepository.Paginate(pageIndex, pageSize, x => x.Key); //order by x => x.Key
            var IQueryableUserWithRoles = users.Select(t => new UserWithRoles()
            {
                User = t,
                Roles = GetUserRoles(t)
            }).AsQueryable();

            return new PaginatedList<UserWithRoles>(users.PageIndex, users.PageSize, users.TotalCount, IQueryableUserWithRoles);
        }

        public bool RemoveFromRole(string userName, string roleName)
        {
            var user = _userRepository.GetSingleByUserName(userName);
            var role = _roleRepository.GetSingleByRoleName(roleName);

            if(user != null && role != null)
            {
                var userInRole = _userInRoleRepository.GetAll().FirstOrDefault(t => t.UserKey == user.Key && t.RoleKey == role.Key);
                if(userInRole != null)
                {
                    _userInRoleRepository.Delete(userInRole);
                    _userInRoleRepository.Save();
                    return true;
                }
            }
            return false;
        }

        public void UpdateUser(User user, string userName, string email) //public UserWithRoles UpdateUser(User user, string userName, string email)
        {
            user.Name = userName;
            user.Email = email;
            user.LastUpdatedOn = DateTime.Now;

            _userRepository.Edit(user);
            _userRepository.Save();
        }

        public ValidUserContext ValidUser(string userName, string password)
        {
            var validUserContext = new ValidUserContext();
            var user = _userRepository.GetSingleByUserName(userName);
            if (user != null && !isUserIsLocked(user, password))
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

        private bool isUserIsLocked(User user, string password)
        {
            if (user.HashedPassword == _cryptoService.EncryptPassword(password, user.Salt)) //IsPassworsIsValid
            {
                return user.IsLocked;
            }
            return false;
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

        private IEnumerable<Role> GetUserRoles(User user)
        {
            var userInRoles = user.UserInRoles;
            if (userInRoles != null && userInRoles.Count > 0)
            {
                var RoleKeysArray = userInRoles.Select(x => x.RoleKey).ToArray();
                var userRoles = _roleRepository.FindBy(x => RoleKeysArray.Contains(x.Key));
                return userRoles;
            }
            return Enumerable.Empty<Role>();
        }
    }
}

