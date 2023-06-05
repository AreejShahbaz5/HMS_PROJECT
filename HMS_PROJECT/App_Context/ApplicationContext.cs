using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HMS_PROJECT.Models;
using HMS_PROJECT.Controllers.Configuration;
using HMS_PROJECT.Models.Setup;
using HMS_PROJECT.Models.Transaction;

namespace HMS_PROJECT.App_Context
{
    public class Application_Context : DbContext
    {
        public Application_Context()
        {

        }

        //Configuration/Setup
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BackUP> Back_UP { get; set; }

        //Setup
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        //Transaction.
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}