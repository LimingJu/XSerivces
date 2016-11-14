﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SharedModel;

namespace SharedConfig
{
    public class DefaultAppDbContext : IdentityDbContext<ServiceUser>
    {
        public DefaultAppDbContext()
            : base("PgDatabaseContext")
        {
        }

        public static DefaultAppDbContext Create()
        {
            return new DefaultAppDbContext();
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
            modelBuilder.Entity<ServiceUser>()
               .HasMany(p => p.BindingSites)
               .WithMany(t => t.BoundServiceUsers)
               .Map(mc =>
               {
                   mc.ToTable("ServiceUserSiteInfo_M2M");
                   mc.MapLeftKey("ServiceUserId");
                   mc.MapRightKey("SiteInfoId");
               });
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<PosTrx> PosTrxModels { get; set; }
        public System.Data.Entity.DbSet<PosItem> PosItemModels { get; set; }
        public System.Data.Entity.DbSet<PosTrxItem> PosTrxItemModels { get; set; }

        public System.Data.Entity.DbSet<PosDiscount> PosDiscountModels { get; set; }

        public System.Data.Entity.DbSet<PosTrxDiscount> PosTrxDiscountModels { get; set; }
        public System.Data.Entity.DbSet<PosStaff> PosStaffModels { get; set; }

        public System.Data.Entity.DbSet<PosMop> PosMopModels { get; set; }
        public System.Data.Entity.DbSet<PosTrxMop> PosTrxMopModels { get; set; }

        public System.Data.Entity.DbSet<SnapShot> SnapShotModels { get; set; }

        public System.Data.Entity.DbSet<Currency> CurrencyModels { get; set; }

        public System.Data.Entity.DbSet<SiteInfo> SiteInfoModels { get; set; }
        public System.Data.Entity.DbSet<IdentityUserClaim> IdentityUserClaimModels { get; set; }
        public System.Data.Entity.DbSet<IdentityUserRole> IdentityUserRoleModels { get; set; }
    }
}
