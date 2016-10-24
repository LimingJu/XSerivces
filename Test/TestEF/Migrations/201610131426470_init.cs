namespace TestEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Detail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PeopleDetails", t => t.Detail_Id)
                .Index(t => t.Name, unique: true, name: "IX_NameAndDetail")
                .Index(t => t.Detail_Id);
            
            CreateTable(
                "dbo.PeopleDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCard = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "Detail_Id", "dbo.PeopleDetails");
            DropIndex("dbo.People", new[] { "Detail_Id" });
            DropIndex("dbo.People", "IX_NameAndDetail");
            DropTable("dbo.PeopleDetails");
            DropTable("dbo.People");
        }
    }
}
