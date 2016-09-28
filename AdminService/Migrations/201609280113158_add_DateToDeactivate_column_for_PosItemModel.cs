namespace AdminService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_DateToDeactivate_column_for_PosItemModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.PosItemModels", "DateToDeactivate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("public.PosItemModels", "DateToDeactivate");
        }
    }
}
