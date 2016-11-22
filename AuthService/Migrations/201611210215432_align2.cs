namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class align2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.PosTrxes", "ServiceIdentityUserId", c => c.String(maxLength: 128));
            CreateIndex("public.PosTrxes", "ServiceIdentityUserId");
            AddForeignKey("public.PosTrxes", "ServiceIdentityUserId", "public.AspNetUsers", "Id");
            DropTable("public.PosStaffs");
        }
        
        public override void Down()
        {
            CreateTable(
                "public.PosStaffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.Binary(),
                        CreatedDateTime = c.DateTime(nullable: false),
                        LastLoginDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("public.PosTrxes", "ServiceIdentityUserId", "public.AspNetUsers");
            DropIndex("public.PosTrxes", new[] { "ServiceIdentityUserId" });
            DropColumn("public.PosTrxes", "ServiceIdentityUserId");
        }
    }
}
