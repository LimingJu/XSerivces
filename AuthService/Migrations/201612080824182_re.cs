namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class re : DbMigration
    {
        public override void Up()
        {
            DropIndex("public.BusinessUnits", new[] { "ParentBusinessUnitId" });
            AlterColumn("public.BusinessUnits", "ParentBusinessUnitId", c => c.Int());
            CreateIndex("public.BusinessUnits", "ParentBusinessUnitId");
        }
        
        public override void Down()
        {
            DropIndex("public.BusinessUnits", new[] { "ParentBusinessUnitId" });
            AlterColumn("public.BusinessUnits", "ParentBusinessUnitId", c => c.Int(nullable: false));
            CreateIndex("public.BusinessUnits", "ParentBusinessUnitId");
        }
    }
}
