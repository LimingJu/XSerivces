namespace AdminService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("public.PosTrxMops", "Name");
        }
        
        public override void Down()
        {
            AddColumn("public.PosTrxMops", "Name", c => c.String());
        }
    }
}
