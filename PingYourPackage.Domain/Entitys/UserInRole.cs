﻿using System;
using PingYourPackage.Domain.Entitys.Core;
using System.ComponentModel.DataAnnotations;


namespace PingYourPackage.Domain.Entitys
{
    public class UserInRole : IEntity
    {
        [Key]
        public Guid Key { get; set; }
        public Guid UserKey { get; set; }
        public Guid RoleKey { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}