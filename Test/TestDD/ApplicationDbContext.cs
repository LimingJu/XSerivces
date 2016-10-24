using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestDD.Model;

namespace TestDD
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public System.Data.Entity.DbSet<PosItemModel> PosItemModels { get; set; }

        public System.Data.Entity.DbSet<PosTransactionModel> PosTransactionModels { get; set; }
    }
}