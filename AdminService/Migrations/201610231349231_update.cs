namespace AdminService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("public.PosTrxMops");
            AddColumn("public.PosDiscounts", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("public.PosTrxMops", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("public.PosTrxMops", "Id");
            DropColumn("public.PosTrxMops", "MId");
        }
        
        public override void Down()
        {
            AddColumn("public.PosTrxMops", "MId", c => c.Int(nullable: false));
            DropPrimaryKey("public.PosTrxMops");
            DropColumn("public.PosTrxMops", "Id");
            DropColumn("public.PosDiscounts", "Value");
            AddPrimaryKey("public.PosTrxMops", "MId");
        }
    }
}
