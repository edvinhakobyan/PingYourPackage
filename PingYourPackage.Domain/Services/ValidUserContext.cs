using System.Security.Principal;

namespace PingYourPackage.Domain.Services.Interfaces
{
    public class ValidUserContext
    {
        public IPrincipal Principal { get; set; }
        public UserWithRoles User { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }
    }
}
