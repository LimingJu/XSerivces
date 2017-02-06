namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restri0 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "public.RestriInBuBizU", newName: "RestriInBuBizU_M2M");
        }
        
        public override void Down()
        {
            RenameTable(name: "public.RestriInBuBizU_M2M", newName: "RestriInBuBizU");
        }
    }
}
