namespace TransactionService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transaction__related_tables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("public.PosItemModels", "PosTransactionDiscountModel_Id", "public.PosTransactionDiscountModels");
            DropForeignKey("public.PosTransactionDiscountModels", "PosTransactionModel_Id", "public.PosTransactionModels");
            DropForeignKey("public.PosItemModels", "PosTransactionModel_Id", "public.PosTransactionModels");
            DropIndex("public.PosItemModels", new[] { "PosTransactionDiscountModel_Id" });
            DropIndex("public.PosItemModels", new[] { "PosTransactionModel_Id" });
            DropIndex("public.PosTransactionDiscountModels", new[] { "PosTransactionModel_Id" });
            DropPrimaryKey("public.PosTransactionModels");
            CreateTable(
                "public.SoldPosItemModels",
                c => new
                    {
                        PosTransactionId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        SoldCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PosTransactionId, t.ItemId })
                .ForeignKey("public.PosItemModels", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("public.PosTransactionModels", t => t.PosTransactionId, cascadeDelete: true)
                .Index(t => t.PosTransactionId)
                .Index(t => t.ItemId);
            
            AddColumn("public.PosTransactionModels", "PosTransactionId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("public.PosTransactionModels", "PosTransactionId");
            DropColumn("public.PosItemModels", "PosTransactionDiscountModel_Id");
            DropColumn("public.PosItemModels", "PosTransactionModel_Id");
            DropColumn("public.PosTransactionModels", "Id");
            DropTable("public.PosTransactionDiscountModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "public.PosTransactionDiscountModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiscountName = c.String(),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PosTransactionModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("public.PosTransactionModels", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("public.PosItemModels", "PosTransactionModel_Id", c => c.Int());
            AddColumn("public.PosItemModels", "PosTransactionDiscountModel_Id", c => c.Int());
            DropForeignKey("public.SoldPosItemModels", "PosTransactionId", "public.PosTransactionModels");
            DropForeignKey("public.SoldPosItemModels", "ItemId", "public.PosItemModels");
            DropIndex("public.SoldPosItemModels", new[] { "ItemId" });
            DropIndex("public.SoldPosItemModels", new[] { "PosTransactionId" });
            DropPrimaryKey("public.PosTransactionModels");
            DropColumn("public.PosTransactionModels", "PosTransactionId");
            DropTable("public.SoldPosItemModels");
            AddPrimaryKey("public.PosTransactionModels", "Id");
            CreateIndex("public.PosTransactionDiscountModels", "PosTransactionModel_Id");
            CreateIndex("public.PosItemModels", "PosTransactionModel_Id");
            CreateIndex("public.PosItemModels", "PosTransactionDiscountModel_Id");
            AddForeignKey("public.PosItemModels", "PosTransactionModel_Id", "public.PosTransactionModels", "Id");
            AddForeignKey("public.PosTransactionDiscountModels", "PosTransactionModel_Id", "public.PosTransactionModels", "Id");
            AddForeignKey("public.PosItemModels", "PosTransactionDiscountModel_Id", "public.PosTransactionDiscountModels", "Id");
        }
    }
}
