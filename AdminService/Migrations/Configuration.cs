using SharedModel;

namespace AdminService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AdminService.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AdminService.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.CurrencyModels.AddOrUpdate(
              p => p.Name,
              new Currency() { Name = "USD" },
              new Currency() { Name = "RMB" },
              new Currency() { Name = "SGD" },
              new Currency() { Name = "JAP" }
            );
            //
        }
    }
}
