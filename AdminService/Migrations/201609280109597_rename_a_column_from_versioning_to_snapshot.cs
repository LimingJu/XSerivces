namespace AdminService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename_a_column_from_versioning_to_snapshot : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "public.PosItemModels", name: "Versioning_Id", newName: "SnapShot_Id");
            RenameIndex(table: "public.PosItemModels", name: "IX_Versioning_Id", newName: "IX_SnapShot_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "public.PosItemModels", name: "IX_SnapShot_Id", newName: "IX_Versioning_Id");
            RenameColumn(table: "public.PosItemModels", name: "SnapShot_Id", newName: "Versioning_Id");
        }
    }
}
