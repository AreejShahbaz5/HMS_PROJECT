namespace HMS_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUser2Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUsers", "UserInsert", c => c.Guid());
            AlterColumn("dbo.ApplicationUsers", "InsertDate", c => c.DateTime());
            AlterColumn("dbo.ApplicationUsers", "UserUpdate", c => c.Guid());
            AlterColumn("dbo.ApplicationUsers", "UpdateDate", c => c.DateTime());
            AlterColumn("dbo.ApplicationUsers", "UserDelete", c => c.Guid());
            AlterColumn("dbo.ApplicationUsers", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUsers", "DeleteDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ApplicationUsers", "UserDelete", c => c.Guid(nullable: false));
            AlterColumn("dbo.ApplicationUsers", "UpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ApplicationUsers", "UserUpdate", c => c.Guid(nullable: false));
            AlterColumn("dbo.ApplicationUsers", "InsertDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ApplicationUsers", "UserInsert", c => c.Guid(nullable: false));
        }
    }
}
