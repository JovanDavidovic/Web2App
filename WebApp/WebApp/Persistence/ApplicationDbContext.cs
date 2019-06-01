using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Models;

namespace WebApp.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<Bus> Buses { get; set; }
        public DbSet<DayType> DayTypes { get; set; }
        public DbSet<DepartureTime> DepartureTimes { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<PassengerType> PassengerTypes { get; set; }
        public DbSet<Pricelist> Pricelists { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketPrice> TicketPrices { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
    }
}