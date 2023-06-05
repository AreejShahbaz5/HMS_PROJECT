namespace HMS_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateroletbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRoles", "Action", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRoles", "Action");
        }
    }
}
