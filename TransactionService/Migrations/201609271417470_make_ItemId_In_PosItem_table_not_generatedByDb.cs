namespace TransactionService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class make_ItemId_In_PosItem_table_not_generatedByDb : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("public.PosItemModels");
            AlterColumn("public.PosItemModels", "ItemId", c => c.Int(nullable: false));
            AddPrimaryKey("public.PosItemModels", "ItemId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("public.PosItemModels");
            AlterColumn("public.PosItemModels", "ItemId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("public.PosItemModels", "ItemId");
        }
    }
}
