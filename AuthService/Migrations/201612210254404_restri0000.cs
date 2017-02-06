namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restri0000 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("public.RestriInBuBizU_M2M", "ServiceIdentityRoleId", "public.AspNetRoles");
            DropForeignKey("public.RestriInBuBizU_M2M", "BusinessUnitId", "public.BusinessUnits");
            DropIndex("public.RestriInBuBizU_M2M", new[] { "ServiceIdentityRoleId" });
            DropIndex("public.RestriInBuBizU_M2M", new[] { "BusinessUnitId" });
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
            
            DropTable("public.RestriInBuBizU_M2M");
        }
        
        public override void Down()
        {
            CreateTable(
                "public.RestriInBuBizU_M2M",
                c => new
                    {
                        ServiceIdentityRoleId = c.String(nullable: false, maxLength: 128),
                        BusinessUnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServiceIdentityRoleId, t.BusinessUnitId });
            
            DropForeignKey("public.IdentityUserBusiUnit_M2M", "BusinessUnitId", "public.BusinessUnits");
            DropForeignKey("public.IdentityUserBusiUnit_M2M", "ServiceIdentityUserId", "public.AspNetUsers");
            DropIndex("public.IdentityUserBusiUnit_M2M", new[] { "BusinessUnitId" });
            DropIndex("public.IdentityUserBusiUnit_M2M", new[] { "ServiceIdentityUserId" });
            DropTable("public.IdentityUserBusiUnit_M2M");
            CreateIndex("public.RestriInBuBizU_M2M", "BusinessUnitId");
            CreateIndex("public.RestriInBuBizU_M2M", "ServiceIdentityRoleId");
            AddForeignKey("public.RestriInBuBizU_M2M", "BusinessUnitId", "public.BusinessUnits", "Id", cascadeDelete: true);
            AddForeignKey("public.RestriInBuBizU_M2M", "ServiceIdentityRoleId", "public.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}
