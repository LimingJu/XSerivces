namespace AdminService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("public.PosTrxMops", new[] { "PosTrxId" });
            AddColumn("public.PosTrxMops", "LineNum", c => c.Int(nullable: false));
            CreateIndex("public.PosTrxMops", new[] { "LineNum", "PosTrxId" }, unique: true, name: "IX_LineNumAndPosTrxId");
        }
        
        public override void Down()
        {
            DropIndex("public.PosTrxMops", "IX_LineNumAndPosTrxId");
            DropColumn("public.PosTrxMops", "LineNum");
            CreateIndex("public.PosTrxMops", "PosTrxId");
        }
    }
}
