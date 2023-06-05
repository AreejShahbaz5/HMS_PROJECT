namespace HMS_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationdatatypeUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "Booking_No", c => c.String());
            AlterColumn("dbo.Reservations", "Status", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "Status", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.Reservations", "Booking_No", c => c.Int(nullable: false));
        }
    }
}
