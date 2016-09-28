namespace AdminService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_PosItemDetail_for_simplify_only_for_demo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("public.PosItemDetailModels", "Versioning_Id", "public.SnapShotModels");
            DropForeignKey("public.PosItemModels", "Detail_ItemBarCode", "public.PosItemDetailModels");
            DropIndex("public.PosItemDetailModels", new[] { "Versioning_Id" });
            DropIndex("public.PosItemModels", new[] { "Detail_ItemBarCode" });
            AddColumn("public.PosItemModels", "ItemBarCode", c => c.String(maxLength: 200));
            AlterColumn("public.PosItemModels", "ItemDepartmentId", c => c.String(maxLength: 100));
            AlterColumn("public.PosItemModels", "TaxItemGroupId", c => c.String(maxLength: 100));
            DropColumn("public.PosItemModels", "Detail_ItemBarCode");
            DropTable("public.PosItemDetailModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "public.PosItemDetailModels",
                c => new
                    {
                        ItemBarCode = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        Versioning_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ItemBarCode);
            
            AddColumn("public.PosItemModels", "Detail_ItemBarCode", c => c.String(maxLength: 128));
            AlterColumn("public.PosItemModels", "TaxItemGroupId", c => c.String());
            AlterColumn("public.PosItemModels", "ItemDepartmentId", c => c.String());
            DropColumn("public.PosItemModels", "ItemBarCode");
            CreateIndex("public.PosItemModels", "Detail_ItemBarCode");
            CreateIndex("public.PosItemDetailModels", "Versioning_Id");
            AddForeignKey("public.PosItemModels", "Detail_ItemBarCode", "public.PosItemDetailModels", "ItemBarCode");
            AddForeignKey("public.PosItemDetailModels", "Versioning_Id", "public.SnapShotModels", "Id");
        }
    }
}
