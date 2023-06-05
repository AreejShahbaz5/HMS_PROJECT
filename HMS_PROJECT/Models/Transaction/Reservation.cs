using HMS_PROJECT.Models.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HMS_PROJECT.Models.Transaction
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }

        public string Booking_No  { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }
        
        [Required]
        public DateTime CheckOut { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
        // Navigation Property
        [ForeignKey("CustomerId")]
        public Customer Customers { get; set; }

        [Required]
        public Guid Roomtype_Id { get; set; }

        // Navigation Property
        [ForeignKey("RoomId")]
        public Room Rooms { get; set; }
        [Required]
        public Guid RoomId { get; set; }

        public int TotalPerson { get; set; }

        [Required]
        public string Status { get; set; }

        public Guid? BookedBy { get; set; }

        public DateTime? BookingDate { get; set; }

        public Guid? UserUpdate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public Guid? BookingCancelBy { get; set; }

        public DateTime? BookingCancelDate { get; set; }

        [NotMapped]
        public IEnumerable<RoomType> RoomTypeCollection { get; set; }
        [NotMapped]
        public IEnumerable<Room> RoomCollection { get; set; }
        [NotMapped]
        public IEnumerable<Customer> CustomerCollection { get; set; }

        [NotMapped]
        public int TotalDays { get; set; }

        [NotMapped]
        public double Total_Payment { get; set; }
        [NotMapped]
        public double UpFront_Amount { get; set; }

        [NotMapped]
        public double Remaining_Balance { get; set; }

    }

}