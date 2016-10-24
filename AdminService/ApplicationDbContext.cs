﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SharedConfig;
using SharedModel;

namespace AdminService.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("PgDatabaseContext")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<PosItem>()
                .HasMany(p => p.DiscountedIn)
                .WithMany(t => t.Targets)
                .Map(mc =>
                {
                    mc.ToTable("PosItemPosDis_M2M");
                    mc.MapLeftKey("PosItemId");
                    mc.MapRightKey("PosDiscountId");
                });
            modelBuilder.Entity<PosTrxDiscount>()
                .HasMany(p => p.Items)
                .WithMany(t => t.InDiscounts)
                .Map(mc =>
                {
                    mc.ToTable("PosTrxDisPosTrxItem_M2M");
                    mc.MapLeftKey("PosTrxDiscountId");
                    mc.MapRightKey("PosTrxItemId");
                });
            //base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<PosItem> PosItemModels { get; set; }

        public System.Data.Entity.DbSet<PosStaff> PosStaffModels { get; set; }

        public System.Data.Entity.DbSet<SnapShot> SnapShotModels { get; set; }

        public System.Data.Entity.DbSet<PosTrx> PosTransactionModels { get; set; }

        public System.Data.Entity.DbSet<Currency> CurrencyModels { get; set; }
    }
}