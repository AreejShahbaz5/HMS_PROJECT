namespace HMS_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblRooms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Room_No = c.String(nullable: false, maxLength: 100),
                        RoomTypeId = c.Guid(nullable: false),
                        Floor = c.Int(nullable: false),
                        Status = c.String(nullable: false),
                        Description = c.String(),
                        Clean = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Action = c.String(nullable: false, maxLength: 2),
                        UserInsert = c.Guid(),
                        InsertDate = c.DateTime(),
                        UserUpdate = c.Guid(),
                        UpdateDate = c.DateTime(),
                        UserDelete = c.Guid(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeId, cascadeDelete: true)
                .Index(t => t.RoomTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "RoomTypeId", "dbo.RoomTypes");
            DropIndex("dbo.Rooms", new[] { "RoomTypeId" });
            DropTable("dbo.Rooms");
        }
    }
}
