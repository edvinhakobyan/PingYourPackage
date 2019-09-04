namespace PingYourPackage.Domain.Migrations
{
    using System;
    using PingYourPackage.Domain.Entitys;
    using PingYourPackage.Domain.Entitys.Core;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<EntitiesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EntitiesContext context)
        {
            context.Roles.AddOrUpdate(role => role.Name,
            new Role { Key = Guid.NewGuid(), Name = "Admin" },
            new Role { Key = Guid.NewGuid(), Name = "Employee" },
            new Role { Key = Guid.NewGuid(), Name = "Affiliate" });

            //=================================================================================================================

        }
    }
}
