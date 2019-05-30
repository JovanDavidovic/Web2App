using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int TicketPriceId { get; set; }
        public TicketPrice TicketPrice { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        public int TotalPrice { get; set; }
    }
}