namespace HMS_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblReservationUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Booking_No", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "Status", c => c.String(nullable: false, maxLength: 2));
            AddColumn("dbo.Reservations", "BookedBy", c => c.Guid());
            AddColumn("dbo.Reservations", "BookingDate", c => c.DateTime());
            AddColumn("dbo.Reservations", "UserUpdate", c => c.Guid());
            AddColumn("dbo.Reservations", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Reservations", "BookingCancelBy", c => c.Guid());
            AddColumn("dbo.Reservations", "BookingCancelDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "BookingCancelDate");
            DropColumn("dbo.Reservations", "BookingCancelBy");
            DropColumn("dbo.Reservations", "UpdateDate");
            DropColumn("dbo.Reservations", "UserUpdate");
            DropColumn("dbo.Reservations", "BookingDate");
            DropColumn("dbo.Reservations", "BookedBy");
            DropColumn("dbo.Reservations", "Status");
            DropColumn("dbo.Reservations", "Booking_No");
        }
    }
}
