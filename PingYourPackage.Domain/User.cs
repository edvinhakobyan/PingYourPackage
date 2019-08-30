using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PingYourPackage.Domain
{
    public class User : IEntity
    {
        [Key]
        public Guid Key { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string HashedPassword { get; set; }
        [Required]
        public string Salt { get; set; }
        public bool IsLocked { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public User()
        {
            UserInRoles = new HashSet<UserInRole>();
        }
    }
}
