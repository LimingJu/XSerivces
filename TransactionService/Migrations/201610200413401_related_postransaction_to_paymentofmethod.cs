namespace TransactionService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class related_postransaction_to_paymentofmethod : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.MethodOfPaymentModels",
                c => new
                    {
                        PaymentTypeId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PaymentTypeId);
            
            AddColumn("public.PosTransactionModels", "MethodOfPaymentId", c => c.Int(nullable: false));
            CreateIndex("public.PosTransactionModels", "MethodOfPaymentId");
            AddForeignKey("public.PosTransactionModels", "MethodOfPaymentId", "public.MethodOfPaymentModels", "PaymentTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("public.PosTransactionModels", "MethodOfPaymentId", "public.MethodOfPaymentModels");
            DropIndex("public.PosTransactionModels", new[] { "MethodOfPaymentId" });
            DropColumn("public.PosTransactionModels", "MethodOfPaymentId");
            DropTable("public.MethodOfPaymentModels");
        }
    }
}
