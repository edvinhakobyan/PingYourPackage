namespace PingYourPackage.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Drawing;
    using System.Linq;
    using System.Linq.Expressions;

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
        }
    }
}
