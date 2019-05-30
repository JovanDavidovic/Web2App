﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Passenger : ApplicationUser
    {
        public int Id { get; set; }
        public string VerificationStatus { get; set; }
        public string Image { get; set; }

        public PassengerType Type { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}