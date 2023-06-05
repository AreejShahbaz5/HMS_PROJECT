namespace HMS_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblbackUP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BackUPs",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BackUPs", "UserId", "dbo.ApplicationUsers");
            DropIndex("dbo.BackUPs", new[] { "UserId" });
            DropTable("dbo.BackUPs");
        }
    }
}
