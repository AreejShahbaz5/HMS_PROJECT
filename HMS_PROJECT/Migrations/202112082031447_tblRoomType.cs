namespace HMS_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblRoomType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Capacity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
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
            DropTable("dbo.RoomTypes");
        }
    }
}
