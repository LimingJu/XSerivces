namespace TransactionService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class align : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "public.PosTransactionDicountModels", newName: "PosTransactionDiscountModels");
            RenameColumn(table: "public.PosItemModels", name: "PosTransactionDicountModel_Id", newName: "PosTransactionDiscountModel_Id");
            RenameIndex(table: "public.PosItemModels", name: "IX_PosTransactionDicountModel_Id", newName: "IX_PosTransactionDiscountModel_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "public.PosItemModels", name: "IX_PosTransactionDiscountModel_Id", newName: "IX_PosTransactionDicountModel_Id");
            RenameColumn(table: "public.PosItemModels", name: "PosTransactionDiscountModel_Id", newName: "PosTransactionDicountModel_Id");
            RenameTable(name: "public.PosTransactionDiscountModels", newName: "PosTransactionDicountModels");
        }
    }
}
