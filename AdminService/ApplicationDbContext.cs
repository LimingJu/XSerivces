using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SharedConfig;

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
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<PeopleModel> PeopleModels { get; set; }
        public System.Data.Entity.DbSet<PeopleDetailModel> PeopleDetailModels { get; set; }
    }
}