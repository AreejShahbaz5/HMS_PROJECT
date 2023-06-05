namespace HMS_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(nullable: false, maxLength: 100),
                        UserRoleId = c.Guid(nullable: false),
                        Active = c.Boolean(nullable: false),
                        LockEnableCount = c.Int(nullable: false),
                        LockEnable = c.Boolean(nullable: false),
                        Action = c.String(nullable: false, maxLength: 2),
                        UserInsert = c.Guid(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                        UserUpdate = c.Guid(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UserDelete = c.Guid(nullable: false),
                        DeleteDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserRoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUsers", "UserRoleId", "dbo.UserRoles");
            DropIndex("dbo.ApplicationUsers", new[] { "UserRoleId" });
            DropTable("dbo.ApplicationUsers");
        }
    }
}
