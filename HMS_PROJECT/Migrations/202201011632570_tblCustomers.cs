namespace HMS_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblCustomers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FullName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Contact = c.String(nullable: false),
                        AnotherContact = c.String(nullable: false),
                        CNIC = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Action = c.String(nullable: false, maxLength: 2),
                        UserInsert = c.Guid(),
                        InsertDate = c.DateTime(),
                        UserUpdate = c.Guid(),
                        UpdateDate = c.DateTime(),
                        UserDelete = c.Guid(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
