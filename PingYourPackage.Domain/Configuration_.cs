namespace PingYourPackage.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration_ : DbMigrationsConfiguration<EntitiesContext>
    {
        public Configuration_()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EntitiesContext context)
        {
            context.Roles.AddOrUpdate(role => role.Name,
            new Role { Key = Guid.NewGuid(), Name = "Admin" },
            new Role { Key = Guid.NewGuid(), Name = "Employee" },
            new Role { Key = Guid.NewGuid(), Name = "Affiliate" });
        }
    }
}
