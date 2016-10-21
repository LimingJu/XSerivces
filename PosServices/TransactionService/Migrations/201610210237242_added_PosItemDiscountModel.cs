namespace TransactionService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_PosItemDiscountModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.PosItemDiscountModels",
                c => new
                    {
                        PosItemDiscountId = c.Int(nullable: false),
                        ReductionMethod = c.Int(nullable: false),
                        ReductionAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PosItemDiscountId);
            
            AddColumn("public.PosItemModels", "PosItemDiscountId", c => c.Int(nullable: false));
            CreateIndex("public.PosItemModels", "PosItemDiscountId");
            AddForeignKey("public.PosItemModels", "PosItemDiscountId", "public.PosItemDiscountModels", "PosItemDiscountId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("public.PosItemModels", "PosItemDiscountId", "public.PosItemDiscountModels");
            DropIndex("public.PosItemModels", new[] { "PosItemDiscountId" });
            DropColumn("public.PosItemModels", "PosItemDiscountId");
            DropTable("public.PosItemDiscountModels");
        }
    }
}
