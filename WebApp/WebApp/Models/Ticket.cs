using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }

        public string PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        public int TotalPrice { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}