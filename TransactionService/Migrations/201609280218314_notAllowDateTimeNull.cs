namespace TransactionService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notAllowDateTimeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.PosItemModels", "DateToActivate", c => c.DateTime(nullable: false));
            AlterColumn("public.PosItemModels", "DateToDeactivate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("public.PosItemModels", "DateToDeactivate", c => c.DateTime());
            AlterColumn("public.PosItemModels", "DateToActivate", c => c.DateTime());
        }
    }
}
