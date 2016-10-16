namespace TransactionService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPosTransactionRelatedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.PosTransactionModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionSource = c.Int(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        ReceiptId = c.String(maxLength: 20),
                        TerminalId = c.Int(nullable: false),
                        ShiftId = c.Int(nullable: false),
                        TransactionInitDateTime = c.DateTime(nullable: false),
                        NetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.PosTransactionDicountModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiscountName = c.String(),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PosTransactionModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.PosTransactionModels", t => t.PosTransactionModel_Id)
                .Index(t => t.PosTransactionModel_Id);
            
            AddColumn("public.PosItemModels", "PosTransactionDicountModel_Id", c => c.Int());
            AddColumn("public.PosItemModels", "PosTransactionModel_Id", c => c.Int());
            CreateIndex("public.PosItemModels", "PosTransactionDicountModel_Id");
            CreateIndex("public.PosItemModels", "PosTransactionModel_Id");
            AddForeignKey("public.PosItemModels", "PosTransactionDicountModel_Id", "public.PosTransactionDicountModels", "Id");
            AddForeignKey("public.PosItemModels", "PosTransactionModel_Id", "public.PosTransactionModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("public.PosItemModels", "PosTransactionModel_Id", "public.PosTransactionModels");
            DropForeignKey("public.PosTransactionDicountModels", "PosTransactionModel_Id", "public.PosTransactionModels");
            DropForeignKey("public.PosItemModels", "PosTransactionDicountModel_Id", "public.PosTransactionDicountModels");
            DropIndex("public.PosTransactionDicountModels", new[] { "PosTransactionModel_Id" });
            DropIndex("public.PosItemModels", new[] { "PosTransactionModel_Id" });
            DropIndex("public.PosItemModels", new[] { "PosTransactionDicountModel_Id" });
            DropColumn("public.PosItemModels", "PosTransactionModel_Id");
            DropColumn("public.PosItemModels", "PosTransactionDicountModel_Id");
            DropTable("public.PosTransactionDicountModels");
            DropTable("public.PosTransactionModels");
        }
    }
}
