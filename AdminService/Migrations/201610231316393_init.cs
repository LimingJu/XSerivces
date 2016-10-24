namespace AdminService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.PosItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.String(nullable: false, maxLength: 20),
                        SnapShotId = c.Int(nullable: false),
                        ItemName = c.String(nullable: false, maxLength: 100),
                        ItemDepartmentId = c.String(maxLength: 100),
                        UnitId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxItemGroupId = c.String(maxLength: 100),
                        DateToActivate = c.DateTime(nullable: false),
                        DateToDeactivate = c.DateTime(nullable: false),
                        BarCode = c.String(maxLength: 200),
                        PLU = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.SnapShots", t => t.SnapShotId, cascadeDelete: true)
                .Index(t => new { t.ItemId, t.SnapShotId }, unique: true, name: "IX_ItemIdAndSnapShotId");
            
            CreateTable(
                "public.PosDiscounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiscountName = c.String(nullable: false),
                        DiscountType = c.Int(nullable: false),
                        DiscountRule = c.Int(nullable: false),
                        SnapShotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.SnapShots", t => t.SnapShotId, cascadeDelete: true)
                .Index(t => new { t.DiscountName, t.SnapShotId }, unique: true, name: "IX_DiscountNameAndSnapShotId");
            
            CreateTable(
                "public.SnapShots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tag = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.PosStaffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.Binary(),
                        CreatedDateTime = c.DateTime(nullable: false),
                        LastLoginDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.PosTrxes",
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
                        CurrencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Currencies", t => t.CurrencyId, cascadeDelete: true)
                .Index(t => t.CurrencyId);
            
            CreateTable(
                "public.PosTrxDiscounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PosDiscountId = c.Int(nullable: false),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PosTrxId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.PosDiscounts", t => t.PosDiscountId, cascadeDelete: true)
                .ForeignKey("public.PosTrxes", t => t.PosTrxId, cascadeDelete: true)
                .Index(t => t.PosDiscountId)
                .Index(t => t.PosTrxId);
            
            CreateTable(
                "public.PosTrxItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LineNum = c.Int(nullable: false),
                        PosItemId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        PosTrxId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.PosItems", t => t.PosItemId, cascadeDelete: true)
                .ForeignKey("public.PosTrxes", t => t.PosTrxId, cascadeDelete: true)
                .Index(t => new { t.LineNum, t.PosTrxId }, unique: true, name: "IX_LineNumAndPosTrxId")
                .Index(t => t.PosItemId);
            
            CreateTable(
                "public.PosTrxMops",
                c => new
                    {
                        MId = c.Int(nullable: false),
                        Name = c.String(),
                        Paid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PayBack = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PosTrxId = c.Int(nullable: false),
                        PosMopId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MId)
                .ForeignKey("public.PosMops", t => t.PosMopId, cascadeDelete: true)
                .ForeignKey("public.PosTrxes", t => t.PosTrxId, cascadeDelete: true)
                .Index(t => t.PosTrxId)
                .Index(t => t.PosMopId);
            
            CreateTable(
                "public.PosMops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        PaymentId = c.Int(nullable: false),
                        SnapShotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.SnapShots", t => t.SnapShotId, cascadeDelete: true)
                .Index(t => new { t.PaymentId, t.SnapShotId }, unique: true, name: "IX_PaymentIdAndSnapShotId");
            
            CreateTable(
                "public.PosItemPosDis_M2M",
                c => new
                    {
                        PosItemId = c.Int(nullable: false),
                        PosDiscountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PosItemId, t.PosDiscountId })
                .ForeignKey("public.PosItems", t => t.PosItemId, cascadeDelete: true)
                .ForeignKey("public.PosDiscounts", t => t.PosDiscountId, cascadeDelete: true)
                .Index(t => t.PosItemId)
                .Index(t => t.PosDiscountId);
            
            CreateTable(
                "public.PosTrxDisPosTrxItem_M2M",
                c => new
                    {
                        PosTrxDiscountId = c.Int(nullable: false),
                        PosTrxItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PosTrxDiscountId, t.PosTrxItemId })
                .ForeignKey("public.PosTrxDiscounts", t => t.PosTrxDiscountId, cascadeDelete: true)
                .ForeignKey("public.PosTrxItems", t => t.PosTrxItemId, cascadeDelete: true)
                .Index(t => t.PosTrxDiscountId)
                .Index(t => t.PosTrxItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.PosTrxMops", "PosTrxId", "public.PosTrxes");
            DropForeignKey("public.PosTrxMops", "PosMopId", "public.PosMops");
            DropForeignKey("public.PosMops", "SnapShotId", "public.SnapShots");
            DropForeignKey("public.PosTrxDiscounts", "PosTrxId", "public.PosTrxes");
            DropForeignKey("public.PosTrxDiscounts", "PosDiscountId", "public.PosDiscounts");
            DropForeignKey("public.PosTrxDisPosTrxItem_M2M", "PosTrxItemId", "public.PosTrxItems");
            DropForeignKey("public.PosTrxDisPosTrxItem_M2M", "PosTrxDiscountId", "public.PosTrxDiscounts");
            DropForeignKey("public.PosTrxItems", "PosTrxId", "public.PosTrxes");
            DropForeignKey("public.PosTrxItems", "PosItemId", "public.PosItems");
            DropForeignKey("public.PosTrxes", "CurrencyId", "public.Currencies");
            DropForeignKey("public.PosItems", "SnapShotId", "public.SnapShots");
            DropForeignKey("public.PosItemPosDis_M2M", "PosDiscountId", "public.PosDiscounts");
            DropForeignKey("public.PosItemPosDis_M2M", "PosItemId", "public.PosItems");
            DropForeignKey("public.PosDiscounts", "SnapShotId", "public.SnapShots");
            DropIndex("public.PosTrxDisPosTrxItem_M2M", new[] { "PosTrxItemId" });
            DropIndex("public.PosTrxDisPosTrxItem_M2M", new[] { "PosTrxDiscountId" });
            DropIndex("public.PosItemPosDis_M2M", new[] { "PosDiscountId" });
            DropIndex("public.PosItemPosDis_M2M", new[] { "PosItemId" });
            DropIndex("public.PosMops", "IX_PaymentIdAndSnapShotId");
            DropIndex("public.PosTrxMops", new[] { "PosMopId" });
            DropIndex("public.PosTrxMops", new[] { "PosTrxId" });
            DropIndex("public.PosTrxItems", new[] { "PosItemId" });
            DropIndex("public.PosTrxItems", "IX_LineNumAndPosTrxId");
            DropIndex("public.PosTrxDiscounts", new[] { "PosTrxId" });
            DropIndex("public.PosTrxDiscounts", new[] { "PosDiscountId" });
            DropIndex("public.PosTrxes", new[] { "CurrencyId" });
            DropIndex("public.PosDiscounts", "IX_DiscountNameAndSnapShotId");
            DropIndex("public.PosItems", "IX_ItemIdAndSnapShotId");
            DropTable("public.PosTrxDisPosTrxItem_M2M");
            DropTable("public.PosItemPosDis_M2M");
            DropTable("public.PosMops");
            DropTable("public.PosTrxMops");
            DropTable("public.PosTrxItems");
            DropTable("public.PosTrxDiscounts");
            DropTable("public.PosTrxes");
            DropTable("public.PosStaffs");
            DropTable("public.SnapShots");
            DropTable("public.PosDiscounts");
            DropTable("public.PosItems");
            DropTable("public.Currencies");
        }
    }
}
