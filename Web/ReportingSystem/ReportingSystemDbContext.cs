using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SharedModel;

namespace ReportingSystem
{
    public class ReportingSystemDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public ReportingSystemDbContext() : base("PgDatabaseContext")
        {
        }

        public System.Data.Entity.DbSet<PosItem> PosItemModels { get; set; }
        public System.Data.Entity.DbSet<PosTrxItem> PosTrxItemModels { get; set; }

        public System.Data.Entity.DbSet<PosStaff> PosStaffModels { get; set; }

        public System.Data.Entity.DbSet<SnapShot> SnapShotModels { get; set; }

        public System.Data.Entity.DbSet<PosDiscount> PosDiscountModels { get; set; }

        public System.Data.Entity.DbSet<PosTrxDiscount> PosTrxDiscountModels { get; set; }
        public System.Data.Entity.DbSet<PosTrx> PosTrxModels { get; set; }

        public System.Data.Entity.DbSet<Currency> CurrencyModels { get; set; }
    }
}
