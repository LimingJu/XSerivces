namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class align3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.PosDiscounts", "CreatedDateTime", c => c.DateTime(nullable: false));
            AddColumn("public.PosItems", "CreatedDateTime", c => c.DateTime(nullable: false));
            AddColumn("public.PosMops", "CreatedDateTime", c => c.DateTime(nullable: false));
            AddColumn("public.SiteInfoes", "CreatedDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("public.SiteInfoes", "CreatedDateTime");
            DropColumn("public.PosMops", "CreatedDateTime");
            DropColumn("public.PosItems", "CreatedDateTime");
            DropColumn("public.PosDiscounts", "CreatedDateTime");
        }
    }
}
