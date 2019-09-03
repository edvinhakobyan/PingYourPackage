using System;

namespace PingYourPackage.Domain.Entitys.Core
{
    public interface IEntity
    {
        Guid Key { get; set; }
    }
}
