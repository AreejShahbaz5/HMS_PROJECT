using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS_PROJECT.Models.Setup
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string AnotherContact { get; set; }
        [Required]
        public string CNIC { get; set; }
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