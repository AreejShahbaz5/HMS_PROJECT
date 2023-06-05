using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS_PROJECT.Models.Setup
{
    public class RoomType
    {
        [Required]
        public Guid Id { get; set; } 
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public double Price { get; set; }

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
    }
}