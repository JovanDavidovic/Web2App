using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Passenger : ApplicationUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string VerificationStatus { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }

        public int TypeId { get; set; }
        public PassengerType Type { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}