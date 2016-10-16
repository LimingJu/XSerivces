namespace TransactionService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_People_Model_which_for_demo_purpose_And_Add_Some_other_models : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("public.PeopleModels", "Detail_Id", "public.PeopleDetailModels");
            DropIndex("public.PeopleModels", new[] { "Detail_Id" });
            CreateTable(
                "public.PosItemDetailModels",
                c => new
                    {
                        ItemBarCode = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        Versioning_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ItemBarCode)
                .ForeignKey("public.SnapShotModels", t => t.Versioning_Id)
                .Index(t => t.Versioning_Id);
            
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
                "public.PosItemModels",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false, maxLength: 100),
                        ItemDepartmentId = c.String(),
                        UnitId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxItemGroupId = c.String(),
                        DateToActivate = c.DateTime(),
                        Detail_ItemBarCode = c.String(maxLength: 128),
                        Versioning_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("public.PosItemDetailModels", t => t.Detail_ItemBarCode)
                .ForeignKey("public.SnapShotModels", t => t.Versioning_Id)
                .Index(t => t.Detail_ItemBarCode)
                .Index(t => t.Versioning_Id);
            
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
            
            DropTable("public.PeopleDetailModels");
            DropTable("public.PeopleModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "public.PeopleModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Detail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.PeopleDetailModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SocialNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("public.PosItemModels", "Versioning_Id", "public.SnapShotModels");
            DropForeignKey("public.PosItemModels", "Detail_ItemBarCode", "public.PosItemDetailModels");
            DropForeignKey("public.PosItemDetailModels", "Versioning_Id", "public.SnapShotModels");
            DropIndex("public.PosItemModels", new[] { "Versioning_Id" });
            DropIndex("public.PosItemModels", new[] { "Detail_ItemBarCode" });
            DropIndex("public.PosItemDetailModels", new[] { "Versioning_Id" });
            DropTable("public.PosStaffModels");
            DropTable("public.PosItemModels");
            DropTable("public.SnapShotModels");
            DropTable("public.PosItemDetailModels");
            CreateIndex("public.PeopleModels", "Detail_Id");
            AddForeignKey("public.PeopleModels", "Detail_Id", "public.PeopleDetailModels", "Id");
        }
    }
}
