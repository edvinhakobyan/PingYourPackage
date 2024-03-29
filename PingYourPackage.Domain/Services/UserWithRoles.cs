﻿using PingYourPackage.Domain.Entitys;
using System.Collections.Generic;

namespace PingYourPackage.Domain.Services
{
    public class UserWithRoles
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
