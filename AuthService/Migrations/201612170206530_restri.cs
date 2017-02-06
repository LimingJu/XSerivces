namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restri : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "public.IdentityRoleBusinessUnit_M2M", newName: "RestriInBuBizU");
        }
        
        public override void Down()
        {
            RenameTable(name: "public.RestriInBuBizU", newName: "IdentityRoleBusinessUnit_M2M");
        }
    }
}
