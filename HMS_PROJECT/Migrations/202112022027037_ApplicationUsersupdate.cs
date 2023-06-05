namespace HMS_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUsersupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "Password", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "Password");
        }
    }
}
