namespace TransactionService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PeopleDetail : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "public.People", newName: "PeopleModels");
            CreateTable(
                "public.PeopleDetailModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SocialNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("public.PeopleModels", "Detail_Id", c => c.Int());
            CreateIndex("public.PeopleModels", "Detail_Id");
            AddForeignKey("public.PeopleModels", "Detail_Id", "public.PeopleDetailModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("public.PeopleModels", "Detail_Id", "public.PeopleDetailModels");
            DropIndex("public.PeopleModels", new[] { "Detail_Id" });
            DropColumn("public.PeopleModels", "Detail_Id");
            DropTable("public.PeopleDetailModels");
            RenameTable(name: "public.PeopleModels", newName: "People");
        }
    }
}
