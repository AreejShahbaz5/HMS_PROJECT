using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS_PROJECT.ViewModel.Setup
{
    public class RoomViewModel
    {

        public string Room_No { get; set; }

        public Guid RoomTypeId { get; set; }

        public int Floor { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public bool Clean { get; set; }

        public bool Active { get; set; }

        public string Action { get; set; }

        public Guid? UserInsert { get; set; }

        public DateTime? InsertDate { get; set; }

        public Guid? UserUpdate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public Guid? UserDelete { get; set; }

        public DateTime? DeleteDate { get; set; }

    }
}