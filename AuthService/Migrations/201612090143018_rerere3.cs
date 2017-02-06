namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rerere3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "public.BusinessUnits", name: "BusinessUnit_Id", newName: "ParentBusinessUnitId");
            RenameIndex(table: "public.BusinessUnits", name: "IX_BusinessUnit_Id", newName: "IX_ParentBusinessUnitId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "public.BusinessUnits", name: "IX_ParentBusinessUnitId", newName: "IX_BusinessUnit_Id");
            RenameColumn(table: "public.BusinessUnits", name: "ParentBusinessUnitId", newName: "BusinessUnit_Id");
        }
    }
}
