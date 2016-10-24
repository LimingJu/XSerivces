namespace TestDD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PosItemModels",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        PosItemDescription = c.String(),
                    })
                .PrimaryKey(t => t.ItemId);
            
            CreateTable(
                "dbo.PosTransactionModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PosTransactionDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PosTransactionModelPosItemModels",
                c => new
                    {
                        PosTransactionModel_Id = c.Int(nullable: false),
                        PosItemModel_ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PosTransactionModel_Id, t.PosItemModel_ItemId })
                .ForeignKey("dbo.PosTransactionModels", t => t.PosTransactionModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.PosItemModels", t => t.PosItemModel_ItemId, cascadeDelete: true)
                .Index(t => t.PosTransactionModel_Id)
                .Index(t => t.PosItemModel_ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PosTransactionModelPosItemModels", "PosItemModel_ItemId", "dbo.PosItemModels");
            DropForeignKey("dbo.PosTransactionModelPosItemModels", "PosTransactionModel_Id", "dbo.PosTransactionModels");
            DropIndex("dbo.PosTransactionModelPosItemModels", new[] { "PosItemModel_ItemId" });
            DropIndex("dbo.PosTransactionModelPosItemModels", new[] { "PosTransactionModel_Id" });
            DropTable("dbo.PosTransactionModelPosItemModels");
            DropTable("dbo.PosTransactionModels");
            DropTable("dbo.PosItemModels");
        }
    }
}
