using HMS_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HMS_PROJECT.Models
{
    public class ApplicationUser
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [Required]
        public Guid UserRoleId { get; set; }
        // Navigation Property
        [ForeignKey("UserRoleId")]
        public UserRole UserRoles { get; set; }

        [Required]
        public bool Active { get; set; }

        public int LockEnableCount { get; set; }

        public bool LockEnable { get; set; }

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
        public IEnumerable<UserRole> UserRolesCollection { get; set; }

    }
}