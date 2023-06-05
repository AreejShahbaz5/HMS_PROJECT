namespace HMS_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblpayment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Pay_No = c.String(),
                        ResrvationId = c.Guid(nullable: false),
                        TotalDays = c.Int(nullable: false),
                        Total_Payment = c.Double(nullable: false),
                        UpFront_Amount = c.Double(nullable: false),
                        Remaining_Balance = c.Double(nullable: false),
                        Action = c.String(nullable: false, maxLength: 2),
                        UserInsert = c.Guid(),
                        InsertDate = c.DateTime(),
                        UserUpdate = c.Guid(),
                        UpdateDate = c.DateTime(),
                        UserDelete = c.Guid(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.ResrvationId, cascadeDelete: true)
                .Index(t => t.ResrvationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "ResrvationId", "dbo.Reservations");
            DropIndex("dbo.Payments", new[] { "ResrvationId" });
            DropTable("dbo.Payments");
        }
    }
}
