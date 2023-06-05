using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HMS_PROJECT.Models.Transaction
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string Pay_No { get; set; }
        
        [Required]
        public Guid ResrvationId { get; set; }
        // Navigation Property
        [ForeignKey("ResrvationId")]
        public Reservation Reservations { get; set; }

        public int TotalDays { get; set; }

        public double Total_Payment { get; set; }

        public double UpFront_Amount { get; set; }

        public double Remaining_Balance { get; set; }

        [Required]
        [StringLength(2)]
        public string Action { get; set; }

        public Guid? UserInsert { get; set; }

        public DateTime? InsertDate { get; set; }

        public Guid? UserUpdate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public Guid? UserDelete { get; set; }

        public DateTime? DeleteDate { get; set; }

        [NotMapped]
        public IEnumerable<Reservation> ReservationCollection { get; set; }
    }
}