namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rere : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "public.BusinessUnits", name: "ParentBusinessUnitId", newName: "ChildBusinessUnitId");
            RenameIndex(table: "public.BusinessUnits", name: "IX_ParentBusinessUnitId", newName: "IX_ChildBusinessUnitId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "public.BusinessUnits", name: "IX_ChildBusinessUnitId", newName: "IX_ParentBusinessUnitId");
            RenameColumn(table: "public.BusinessUnits", name: "ChildBusinessUnitId", newName: "ParentBusinessUnitId");
        }
    }
}
