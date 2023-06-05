﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS_PROJECT.Models
{
    public class UserRole
    {
        public Guid ID { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
        public string Action { get; set; }

    }
}