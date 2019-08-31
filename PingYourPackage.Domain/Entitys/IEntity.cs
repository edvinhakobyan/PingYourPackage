using System;

namespace PingYourPackage.Domain
{
    interface IEntity
    {
        Guid Key { get; set; }
    }
}
