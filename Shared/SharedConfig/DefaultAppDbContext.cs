using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SharedModel;
using SharedModel.Identity;

namespace SharedConfig
{
    public class DefaultAppDbContext : IdentityDbContext<ServiceIdentityUser, ServiceIdentityRole, string, IdentityUserLogin, ServiceIdentityUserRole, ServiceIdentityUserClaim>
    //IdentityDbContext<ServiceUser>
    {
        public DefaultAppDbContext()
            : base("PgDatabaseContext")
        {
            Configuration.ProxyCreationEnabled = false;
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
            modelBuilder.Entity<ServiceIdentityUser>()
               .HasMany(p => p.RestrictedInBusinessUnits)
               .WithMany(t => t.ReferencedInUsers)
               .Map(mc =>
               {
                   mc.ToTable("IdentityUserBusiUnit_M2M");
                   mc.MapLeftKey("ServiceIdentityUserId");
                   mc.MapRightKey("BusinessUnitId");
               });
            modelBuilder.Entity<ServiceIdentityRole>()
                .HasMany(p => p.ProhibitedOperations)
                .WithMany(t => t.ProhibitedInIdentityRoles)
                .Map(mc =>
                {
                    mc.ToTable("IdentityRoleUserOperation_M2M");
                    mc.MapLeftKey("ServiceIdentityRoleId");
                    mc.MapRightKey("ServiceUserOperationId");
                });
            // bug???
            //EntityTypeConfiguration<ServiceIdentityUser> entityTypeConfiguration = modelBuilder.Entity<ServiceIdentityUser>().ToTable("AspNetUsers");
            //entityTypeConfiguration.HasMany(u => u.ReadBooks).WithRequired().HasForeignKey(f => f.UserId);

            //EntityTypeConfiguration<ServiceIdentityUser> entityTypeConfiguration = modelBuilder.Entity<ServiceIdentityUser>().ToTable("AspNetUsers");
            //StringPropertyConfiguration arg_273_0 = entityTypeConfiguration.Property((ServiceIdentityUser u) => u.UserName).IsRequired().HasMaxLength(new int?(256));
            //string arg_273_1 = "Index";
            //IndexAttribute indexAttribute = new IndexAttribute("UserNameIndex");
            //indexAttribute.IsUnique=true;
            //arg_273_0.HasColumnAnnotation(arg_273_1, new IndexAnnotation(indexAttribute));
            //entityTypeConfiguration.Property((ServiceIdentityUser u) => u.Email).HasMaxLength(new int?(256));
            //modelBuilder.Entity<ServiceIdentityUserRole>().HasKey((ServiceIdentityUserRole r) => new
            //{
            //    r.UserId,
            //    r.RoleId
            //}).ToTable("AspNetUserRoles");
            //modelBuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) => new
            //{
            //    l.LoginProvider,
            //    l.ProviderKey,
            //    l.UserId
            //}).ToTable("AspNetUserLogins");
            //modelBuilder.Entity<ServiceIdentityUserClaim>().ToTable("AspNetUserClaims");
            //EntityTypeConfiguration<ServiceIdentityRole> entityTypeConfiguration2 = modelBuilder.Entity<ServiceIdentityRole>().ToTable("AspNetRoles");
            //StringPropertyConfiguration arg_563_0 = entityTypeConfiguration2.Property((ServiceIdentityRole r) => r.Name).IsRequired().HasMaxLength(new int?(256));
            //string arg_563_1 = "Index";
            //IndexAttribute indexAttribute2 = new IndexAttribute("RoleNameIndex");
            //indexAttribute2.IsUnique = true;
            //arg_563_0.HasColumnAnnotation(arg_563_1, new IndexAnnotation(indexAttribute2));
            //entityTypeConfiguration2.HasMany<ServiceIdentityUserRole>((ServiceIdentityRole r) => r.Users).WithRequired().HasForeignKey<string>((ServiceIdentityUserRole ur) => ur.RoleId);
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<PosTrx> PosTrxModels { get; set; }
        public System.Data.Entity.DbSet<PosItem> PosItemModels { get; set; }
        public System.Data.Entity.DbSet<PosTrxItem> PosTrxItemModels { get; set; }

        public System.Data.Entity.DbSet<PosDiscount> PosDiscountModels { get; set; }

        public System.Data.Entity.DbSet<PosTrxDiscount> PosTrxDiscountModels { get; set; }

        public System.Data.Entity.DbSet<PosMop> PosMopModels { get; set; }
        public System.Data.Entity.DbSet<PosTrxMop> PosTrxMopModels { get; set; }

        public System.Data.Entity.DbSet<SnapShot> SnapShotModels { get; set; }

        public System.Data.Entity.DbSet<Currency> CurrencyModels { get; set; }

        public System.Data.Entity.DbSet<BusinessUnit> BusinessUnitModels { get; set; }
        public System.Data.Entity.DbSet<ServiceIdentityUserClaim> ServiceIdentityUserClaimModels { get; set; }
        public System.Data.Entity.DbSet<IdentityUserLogin> IdentityUserLoginModels { get; set; }
        public System.Data.Entity.DbSet<ServiceIdentityUserRole> ServiceIdentityUserRoleModels { get; set; }

        public System.Data.Entity.DbSet<ServiceUserOperation> ServiceUserOperationModels { get; set; }
    }
}
