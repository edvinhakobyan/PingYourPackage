using System;
using PingYourPackage.Domain.Entitys.Core;
using System.ComponentModel.DataAnnotations;

namespace PingYourPackage.Domain.Entitys
{
    public class ShipmentState : IEntity
    {
        [Key]
        public Guid Key { get; set; }
        public Guid ShipmentKey { get; set; }
        [Required]
        public ShipmentStatus ShipmentStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public Shipment Shipment { get; set; }
    }

    public enum ShipmentStatus
    {
        Ordered = 1,
        Scheduled = 2,
        InTransit = 3,
        Delivered = 4
    }

}
