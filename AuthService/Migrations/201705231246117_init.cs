namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.BusinessUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDateTime = c.DateTime(),
                        UnitType = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        Address = c.String(),
                        Description = c.String(),
                        ParentBusinessUnitId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.BusinessUnits", t => t.ParentBusinessUnitId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ParentBusinessUnitId);
            
            CreateTable(
                "public.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(maxLength: 50),
                        Address = c.String(),
                        City = c.String(),
                        Description = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Alias, unique: true);
            
            CreateTable(
                "public.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "public.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("public.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "public.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("public.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("public.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "public.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ParentRoleId = c.String(maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.AspNetRoles", t => t.ParentRoleId)
                .Index(t => t.ParentRoleId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "public.ServiceUserOperations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OperationName = c.String(),
                        CreatedDateTime = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.OperationName, unique: true);
            
            CreateTable(
                "public.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_CurrencyName");
            
            CreateTable(
                "public.PosDiscounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiscountName = c.String(nullable: false),
                        DiscountType = c.Int(nullable: false),
                        DiscountRule = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDateTime = c.DateTime(nullable: false),
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
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.SnapShots", t => t.SnapShotId, cascadeDelete: true)
                .Index(t => new { t.ItemId, t.SnapShotId }, unique: true, name: "IX_ItemIdAndSnapShotId");
            
            CreateTable(
                "public.PosMops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        PaymentId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        SnapShotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.SnapShots", t => t.SnapShotId, cascadeDelete: true)
                .Index(t => new { t.PaymentId, t.SnapShotId }, unique: true, name: "IX_PaymentIdAndSnapShotId");
            
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
                .ForeignKey("public.PosTrxes", t => t.PosTrxId, cascadeDelete: true)
                .ForeignKey("public.PosDiscounts", t => t.PosDiscountId, cascadeDelete: true)
                .Index(t => t.PosDiscountId)
                .Index(t => t.PosTrxId);
            
            CreateTable(
                "public.PosTrxItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LineNum = c.Int(nullable: false),
                        PosItemId = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PosTrxId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.PosItems", t => t.PosItemId, cascadeDelete: true)
                .ForeignKey("public.PosTrxes", t => t.PosTrxId, cascadeDelete: true)
                .Index(t => new { t.LineNum, t.PosTrxId }, unique: true, name: "IX_LineNumAndPosTrxId")
                .Index(t => t.PosItemId);
            
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
                        TransactionStatus = c.Int(nullable: false),
                        ServiceIdentityUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.AspNetUsers", t => t.ServiceIdentityUserId)
                .ForeignKey("public.Currencies", t => t.CurrencyId, cascadeDelete: true)
                .Index(t => t.CurrencyId)
                .Index(t => t.ServiceIdentityUserId);
            
            CreateTable(
                "public.PosTrxMops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LineNum = c.Int(nullable: false),
                        Paid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PayBack = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PosTrxId = c.Int(nullable: false),
                        PosMopId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.PosMops", t => t.PosMopId, cascadeDelete: true)
                .ForeignKey("public.PosTrxes", t => t.PosTrxId, cascadeDelete: true)
                .Index(t => new { t.LineNum, t.PosTrxId }, unique: true, name: "IX_LineNumAndPosTrxId")
                .Index(t => t.PosMopId);
            
            CreateTable(
                "public.IdentityUserBusiUnit_M2M",
                c => new
                    {
                        ServiceIdentityUserId = c.String(nullable: false, maxLength: 128),
                        BusinessUnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServiceIdentityUserId, t.BusinessUnitId })
                .ForeignKey("public.AspNetUsers", t => t.ServiceIdentityUserId, cascadeDelete: true)
                .ForeignKey("public.BusinessUnits", t => t.BusinessUnitId, cascadeDelete: true)
                .Index(t => t.ServiceIdentityUserId)
                .Index(t => t.BusinessUnitId);
            
            CreateTable(
                "public.IdentityRoleUserOperation_M2M",
                c => new
                    {
                        ServiceIdentityRoleId = c.String(nullable: false, maxLength: 128),
                        ServiceUserOperationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServiceIdentityRoleId, t.ServiceUserOperationId })
                .ForeignKey("public.AspNetRoles", t => t.ServiceIdentityRoleId, cascadeDelete: true)
                .ForeignKey("public.ServiceUserOperations", t => t.ServiceUserOperationId, cascadeDelete: true)
                .Index(t => t.ServiceIdentityRoleId)
                .Index(t => t.ServiceUserOperationId);
            
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
            DropForeignKey("public.PosTrxDiscounts", "PosDiscountId", "public.PosDiscounts");
            DropForeignKey("public.PosTrxDisPosTrxItem_M2M", "PosTrxItemId", "public.PosTrxItems");
            DropForeignKey("public.PosTrxDisPosTrxItem_M2M", "PosTrxDiscountId", "public.PosTrxDiscounts");
            DropForeignKey("public.PosTrxMops", "PosTrxId", "public.PosTrxes");
            DropForeignKey("public.PosTrxMops", "PosMopId", "public.PosMops");
            DropForeignKey("public.PosTrxItems", "PosTrxId", "public.PosTrxes");
            DropForeignKey("public.PosTrxDiscounts", "PosTrxId", "public.PosTrxes");
            DropForeignKey("public.PosTrxes", "CurrencyId", "public.Currencies");
            DropForeignKey("public.PosTrxes", "ServiceIdentityUserId", "public.AspNetUsers");
            DropForeignKey("public.PosTrxItems", "PosItemId", "public.PosItems");
            DropForeignKey("public.PosMops", "SnapShotId", "public.SnapShots");
            DropForeignKey("public.PosItems", "SnapShotId", "public.SnapShots");
            DropForeignKey("public.PosItemPosDis_M2M", "PosDiscountId", "public.PosDiscounts");
            DropForeignKey("public.PosItemPosDis_M2M", "PosItemId", "public.PosItems");
            DropForeignKey("public.PosDiscounts", "SnapShotId", "public.SnapShots");
            DropForeignKey("public.AspNetUserRoles", "UserId", "public.AspNetUsers");
            DropForeignKey("public.AspNetUserRoles", "RoleId", "public.AspNetRoles");
            DropForeignKey("public.IdentityRoleUserOperation_M2M", "ServiceUserOperationId", "public.ServiceUserOperations");
            DropForeignKey("public.IdentityRoleUserOperation_M2M", "ServiceIdentityRoleId", "public.AspNetRoles");
            DropForeignKey("public.AspNetRoles", "ParentRoleId", "public.AspNetRoles");
            DropForeignKey("public.IdentityUserBusiUnit_M2M", "BusinessUnitId", "public.BusinessUnits");
            DropForeignKey("public.IdentityUserBusiUnit_M2M", "ServiceIdentityUserId", "public.AspNetUsers");
            DropForeignKey("public.AspNetUserLogins", "UserId", "public.AspNetUsers");
            DropForeignKey("public.AspNetUserClaims", "UserId", "public.AspNetUsers");
            DropForeignKey("public.BusinessUnits", "ParentBusinessUnitId", "public.BusinessUnits");
            DropIndex("public.PosTrxDisPosTrxItem_M2M", new[] { "PosTrxItemId" });
            DropIndex("public.PosTrxDisPosTrxItem_M2M", new[] { "PosTrxDiscountId" });
            DropIndex("public.PosItemPosDis_M2M", new[] { "PosDiscountId" });
            DropIndex("public.PosItemPosDis_M2M", new[] { "PosItemId" });
            DropIndex("public.IdentityRoleUserOperation_M2M", new[] { "ServiceUserOperationId" });
            DropIndex("public.IdentityRoleUserOperation_M2M", new[] { "ServiceIdentityRoleId" });
            DropIndex("public.IdentityUserBusiUnit_M2M", new[] { "BusinessUnitId" });
            DropIndex("public.IdentityUserBusiUnit_M2M", new[] { "ServiceIdentityUserId" });
            DropIndex("public.PosTrxMops", new[] { "PosMopId" });
            DropIndex("public.PosTrxMops", "IX_LineNumAndPosTrxId");
            DropIndex("public.PosTrxes", new[] { "ServiceIdentityUserId" });
            DropIndex("public.PosTrxes", new[] { "CurrencyId" });
            DropIndex("public.PosTrxItems", new[] { "PosItemId" });
            DropIndex("public.PosTrxItems", "IX_LineNumAndPosTrxId");
            DropIndex("public.PosTrxDiscounts", new[] { "PosTrxId" });
            DropIndex("public.PosTrxDiscounts", new[] { "PosDiscountId" });
            DropIndex("public.PosMops", "IX_PaymentIdAndSnapShotId");
            DropIndex("public.PosItems", "IX_ItemIdAndSnapShotId");
            DropIndex("public.PosDiscounts", "IX_DiscountNameAndSnapShotId");
            DropIndex("public.Currencies", "IX_CurrencyName");
            DropIndex("public.ServiceUserOperations", new[] { "OperationName" });
            DropIndex("public.AspNetRoles", "RoleNameIndex");
            DropIndex("public.AspNetRoles", new[] { "ParentRoleId" });
            DropIndex("public.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("public.AspNetUserRoles", new[] { "UserId" });
            DropIndex("public.AspNetUserLogins", new[] { "UserId" });
            DropIndex("public.AspNetUserClaims", new[] { "UserId" });
            DropIndex("public.AspNetUsers", new[] { "Alias" });
            DropIndex("public.AspNetUsers", "UserNameIndex");
            DropIndex("public.BusinessUnits", new[] { "ParentBusinessUnitId" });
            DropIndex("public.BusinessUnits", new[] { "Name" });
            DropTable("public.PosTrxDisPosTrxItem_M2M");
            DropTable("public.PosItemPosDis_M2M");
            DropTable("public.IdentityRoleUserOperation_M2M");
            DropTable("public.IdentityUserBusiUnit_M2M");
            DropTable("public.PosTrxMops");
            DropTable("public.PosTrxes");
            DropTable("public.PosTrxItems");
            DropTable("public.PosTrxDiscounts");
            DropTable("public.PosMops");
            DropTable("public.PosItems");
            DropTable("public.SnapShots");
            DropTable("public.PosDiscounts");
            DropTable("public.Currencies");
            DropTable("public.ServiceUserOperations");
            DropTable("public.AspNetRoles");
            DropTable("public.AspNetUserRoles");
            DropTable("public.AspNetUserLogins");
            DropTable("public.AspNetUserClaims");
            DropTable("public.AspNetUsers");
            DropTable("public.BusinessUnits");
        }
    }
}
