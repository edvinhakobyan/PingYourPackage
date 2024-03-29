﻿using System;
using PingYourPackage.Domain.Entitys.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PingYourPackage.Domain.Entitys
{
    public class Affiliate : IEntity
    {

        [Key]
        public Guid Key { get; set; }
        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        [StringLength(50)]
        public string TelephoneNumber { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public User User { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }

        public Affiliate()
        {
            Shipments = new HashSet<Shipment>();
        }

    }
}
