namespace TransactionService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.MethodOfPaymentModels",
                c => new
                    {
                        MethodOfPaymentId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MethodOfPaymentId);
            
            CreateTable(
                "public.PosTransactionModels",
                c => new
                    {
                        PosTransactionId = c.Int(nullable: false, identity: true),
                        TransactionSource = c.Int(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        ReceiptId = c.String(maxLength: 20),
                        TerminalId = c.Int(nullable: false),
                        ShiftId = c.Int(nullable: false),
                        TransactionInitDateTime = c.DateTime(nullable: false),
                        NetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
                        MethodOfPaymentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PosTransactionId)
                .ForeignKey("public.MethodOfPaymentModels", t => t.MethodOfPaymentId, cascadeDelete: true)
                .Index(t => t.MethodOfPaymentId);
            
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
            
            CreateTable(
                "public.PosItemModels",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        ItemName = c.String(nullable: false, maxLength: 100),
                        ItemDepartmentId = c.String(maxLength: 100),
                        UnitId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxItemGroupId = c.String(maxLength: 100),
                        DateToActivate = c.DateTime(nullable: false),
                        DateToDeactivate = c.DateTime(nullable: false),
                        ItemBarCode = c.String(maxLength: 200),
                        SnapShot_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("public.SnapShotModels", t => t.SnapShot_Id)
                .Index(t => t.SnapShot_Id);
            
            CreateTable(
                "public.SnapShotModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tag = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.PosStaffModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.Binary(),
                        CreatedDateTime = c.DateTime(nullable: false),
                        LastLoginDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.SoldPosItemModels", "PosTransactionId", "public.PosTransactionModels");
            DropForeignKey("public.SoldPosItemModels", "ItemId", "public.PosItemModels");
            DropForeignKey("public.PosItemModels", "SnapShot_Id", "public.SnapShotModels");
            DropForeignKey("public.PosTransactionModels", "MethodOfPaymentId", "public.MethodOfPaymentModels");
            DropIndex("public.PosItemModels", new[] { "SnapShot_Id" });
            DropIndex("public.SoldPosItemModels", new[] { "ItemId" });
            DropIndex("public.SoldPosItemModels", new[] { "PosTransactionId" });
            DropIndex("public.PosTransactionModels", new[] { "MethodOfPaymentId" });
            DropTable("public.PosStaffModels");
            DropTable("public.SnapShotModels");
            DropTable("public.PosItemModels");
            DropTable("public.SoldPosItemModels");
            DropTable("public.PosTransactionModels");
            DropTable("public.MethodOfPaymentModels");
        }
    }
}
