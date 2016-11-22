namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class align1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("public.TestReadBooks", "UserId", "public.AspNetUsers");
            DropForeignKey("public.AspNetUserClaims", "UserId", "public.AspNetUsers");
            DropForeignKey("public.AspNetUserLogins", "ServiceIdentityUser_Id", "public.AspNetUsers");
            DropIndex("public.AspNetUserLogins", new[] { "ServiceIdentityUser_Id" });
            DropIndex("public.AspNetUserClaims", new[] { "UserId" });
            DropIndex("public.TestReadBooks", new[] { "UserId" });
            DropColumn("public.AspNetUserLogins", "UserId");
            RenameColumn(table: "public.AspNetUserLogins", name: "ServiceIdentityUser_Id", newName: "UserId");
            DropPrimaryKey("public.AspNetUserLogins");
            AlterColumn("public.AspNetUserLogins", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("public.SnapShots", "CreatedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("public.PosItems", "DateToActivate", c => c.DateTime(nullable: false));
            AlterColumn("public.PosItems", "DateToDeactivate", c => c.DateTime(nullable: false));
            AlterColumn("public.PosStaffs", "CreatedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("public.PosStaffs", "LastLoginDateTime", c => c.DateTime(nullable: false));
            AlterColumn("public.PosTrxes", "TransactionInitDateTime", c => c.DateTime(nullable: false));
            AlterColumn("public.AspNetRoles", "CreatedDateTime", c => c.DateTime());
            AlterColumn("public.AspNetUsers", "CreatedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("public.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("public.AspNetUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("public.AspNetUserLogins", new[] { "LoginProvider", "ProviderKey", "UserId" });
            CreateIndex("public.AspNetUserLogins", "UserId");
            CreateIndex("public.AspNetUserClaims", "UserId");
            AddForeignKey("public.AspNetUserClaims", "UserId", "public.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("public.AspNetUserLogins", "UserId", "public.AspNetUsers", "Id", cascadeDelete: true);
            DropTable("public.TestReadBooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "public.TestReadBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookName = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("public.AspNetUserLogins", "UserId", "public.AspNetUsers");
            DropForeignKey("public.AspNetUserClaims", "UserId", "public.AspNetUsers");
            DropIndex("public.AspNetUserClaims", new[] { "UserId" });
            DropIndex("public.AspNetUserLogins", new[] { "UserId" });
            DropPrimaryKey("public.AspNetUserLogins");
            AlterColumn("public.AspNetUserClaims", "UserId", c => c.String(maxLength: 128));
            AlterColumn("public.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("public.AspNetUsers", "CreatedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("public.AspNetRoles", "CreatedDateTime", c => c.DateTime());
            AlterColumn("public.PosTrxes", "TransactionInitDateTime", c => c.DateTime(nullable: false));
            AlterColumn("public.PosStaffs", "LastLoginDateTime", c => c.DateTime(nullable: false));
            AlterColumn("public.PosStaffs", "CreatedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("public.PosItems", "DateToDeactivate", c => c.DateTime(nullable: false));
            AlterColumn("public.PosItems", "DateToActivate", c => c.DateTime(nullable: false));
            AlterColumn("public.SnapShots", "CreatedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("public.AspNetUserLogins", "UserId", c => c.String(maxLength: 128));
            AddPrimaryKey("public.AspNetUserLogins", new[] { "LoginProvider", "ProviderKey", "UserId" });
            RenameColumn(table: "public.AspNetUserLogins", name: "UserId", newName: "ServiceIdentityUser_Id");
            AddColumn("public.AspNetUserLogins", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("public.TestReadBooks", "UserId");
            CreateIndex("public.AspNetUserClaims", "UserId");
            CreateIndex("public.AspNetUserLogins", "ServiceIdentityUser_Id");
            AddForeignKey("public.AspNetUserLogins", "ServiceIdentityUser_Id", "public.AspNetUsers", "Id");
            AddForeignKey("public.AspNetUserClaims", "UserId", "public.AspNetUsers", "Id");
            AddForeignKey("public.TestReadBooks", "UserId", "public.AspNetUsers", "Id");
        }
    }
}
