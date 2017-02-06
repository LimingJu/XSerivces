namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rerere : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "public.BusinessUnits", name: "ChildBusinessUnitId", newName: "BusinessUnit_Id");
            RenameIndex(table: "public.BusinessUnits", name: "IX_ChildBusinessUnitId", newName: "IX_BusinessUnit_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "public.BusinessUnits", name: "IX_BusinessUnit_Id", newName: "IX_ChildBusinessUnitId");
            RenameColumn(table: "public.BusinessUnits", name: "BusinessUnit_Id", newName: "ChildBusinessUnitId");
        }
    }
}
