using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HMS_PROJECT.Models.Setup
{
    public class Room
    {
        [Required]
        public Guid Id { get; set; } 
        [Required]
        [StringLength(100)]
        public string Room_No { get; set; }

        [Required]
        public Guid RoomTypeId { get; set; }
        // Navigation Property
        [ForeignKey("RoomTypeId")]
        public RoomType RoomTypes { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public string Status { get; set; }

        public string Description { get; set; }

        [Required]
        public bool Clean { get; set; }

        [Required]
        public bool Active { get; set; }

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
        public IEnumerable<RoomType> RoomTypeCollection { get; set; }
    }
    public enum Status
    {
        Available,
        Reserved
    }
}