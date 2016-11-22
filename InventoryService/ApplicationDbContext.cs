using System.Data.Entity;
using SharedModel;

namespace InventoryService
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

        /// <summary>
        /// set of product items
        /// </summary>
        public DbSet<PosItem> PosItemModels { get; set; }
    }
}