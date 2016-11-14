using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using SharedConfig;
using SharedModel;

namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DefaultAppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DefaultAppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.CurrencyModels.AddOrUpdate(new Currency() { Name = "CNY" });
            context.CurrencyModels.AddOrUpdate(new Currency() { Name = "JAP" });
            context.CurrencyModels.AddOrUpdate(new Currency() { Name = "SGD" });
            context.CurrencyModels.AddOrUpdate(new Currency() { Name = "USD" });
            context.SiteInfoModels.AddOrUpdate(new SiteInfo()
            {
                Description = "created for testing",
                Name = "EUCN",
                ProjectCodeName = "EMSG"
            });

            context.SiteInfoModels.AddOrUpdate(new SiteInfo()
            {
                Description = "created for testing",
                Name = "ESBB",
                ProjectCodeName = "EMSG"
            });
            //context.IdentityUserClaimModels.AddOrUpdate(new IdentityUserClaim()
            //{
            //    ClaimType ="ChargeGroup",
            //    ClaimValue = "LimitedFunctionGroup0"
            //});
            //context.Users.AddOrUpdate(new ServiceUser()
            //{
            //    Address = "BaoShanArea",
            //    City = "ShangHai",
            //    CreatedDateTime = DateTime.Now,
                
                
            //});
        }
    }
}
