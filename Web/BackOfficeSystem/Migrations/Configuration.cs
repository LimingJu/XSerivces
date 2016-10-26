using SharedModel;

namespace BackOfficeSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BackOfficeSystem.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BackOfficeSystem.ApplicationDbContext context)
        {
            context.CurrencyModels.AddOrUpdate(
             p => p.Name,
             new Currency() { Name = "USD" },
             new Currency() { Name = "RMB" },
             new Currency() { Name = "SGD" },
             new Currency() { Name = "JAP" }
           );
        }
    }
}
