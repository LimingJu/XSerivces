using System.Collections.Generic;
using TestDD.Model;

namespace TestDD.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TestDD.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TestDD.ApplicationDbContext context)
        {
            PosItemModel posItem = new PosItemModel() { ItemId = 1, PosItemDescription = "1st PosItem" };
            var posTrx1st = new PosTransactionModel()
            {
                PosTransactionDescription = "1st PosTrx",
                SaleItems = new List<PosItemModel>() { posItem }
            };
            var posTrx2nd = new PosTransactionModel()
            {
                PosTransactionDescription = "2nd PosTrx",
                SaleItems = new List<PosItemModel>() { posItem }
            };
            context.PosTransactionModels.Add(posTrx1st);
            context.PosTransactionModels.Add(posTrx2nd);


            var target = context.PosTransactionModels.Include(trx => trx.SaleItems)
                                       .First(trx => trx.Id == 1);
            target.SaleItems.Add(childEntity);
            db.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
