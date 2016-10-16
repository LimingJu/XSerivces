using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SharedConfig;
using SharedModel;

namespace TransactionService.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("PgDatabaseContext")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<PosItemModel> PosItemModels { get; set; }

        public System.Data.Entity.DbSet<PosStaffModel> PosStaffModels { get; set; }

        public System.Data.Entity.DbSet<SnapShotModel> SnapShotModels { get; set; }

        public System.Data.Entity.DbSet<PosTransactionModel> PosTransactionModels { get; set; }

        public DbSet<SoldPosItemModel> SoldPosItemModels { get; set; }
    }
}