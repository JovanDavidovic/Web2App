using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Ticket")]
    public class TicketsController : ApiController
    {
        public IUnitOfWork DB { get; set; }


        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public TicketsController(IUnitOfWork db)
        {
            this.DB = db;
        }

        [AllowAnonymous]
        // GET: api/Tickets
        public IEnumerable<Ticket> GetTickets()
        {
            var ret = DB.TicketRepository.GetAll().ToList();
            return ret;
        }

        // GET: api/Tickets/5
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult GetTicket(int id)
        {
            //Ticket ticket = db.Tickets.Find(id);
            //if (ticket == null)
            //{
            //    return NotFound();
            //}

            return Ok();
        }

        // PUT: api/Tickets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTicket(int id, Ticket ticket)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != ticket.Id)
            //{
            //    return BadRequest();
            //}

            //db.Entry(ticket).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!TicketExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return Ok();
        }

        // POST: api/Tickets
        [AllowAnonymous]
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult PostTicket(BuyTicketBindingModel ticket)
        {
            Passenger passenger = DB.PassengerRepository.Find(p => p.UserName == ticket.Username).FirstOrDefault();

            if (passenger == null)
            {
                Pricelist pricelist = DB.PricelistRepository.Find(p => p.From <= DateTime.Now && p.To >= DateTime.Now).FirstOrDefault();

                if (pricelist == null)
                {
                    ModelState.AddModelError("", "Pricelist is not defined for this period.");
                    return BadRequest(ModelState);
                }

                var ticketPrices = DB.TicketPriceRepository.Find(tp => tp.PricelistId == pricelist.Id).ToList();
                ticket.Hour = ticketPrices[0].Price; ;
                ticket.Day = ticketPrices[1].Price; ;
                ticket.Month = ticketPrices[2].Price; ;
                ticket.Year = ticketPrices[3].Price; ;

                return Ok(ticket);
            }

            if (passenger.VerificationStatus == "ACCEPTED")
            {
                PassengerType pType = DB.PassengerTypeRepository.Get(passenger.TypeId);
                Pricelist pricelist = DB.PricelistRepository.Find(p => p.From <= DateTime.Now && p.To >= DateTime.Now).FirstOrDefault();

                if (pricelist == null)
                {
                    ModelState.AddModelError("", "Pricelist is not defined for this period.");
                    return BadRequest(ModelState);
                }

                var ticketPrices = DB.TicketPriceRepository.Find(tp => tp.PricelistId == pricelist.Id).ToList();
                ticket.Hour = (ticketPrices[0].Price * pType.Coefficient) / 100;
                ticket.Day = (ticketPrices[1].Price * pType.Coefficient) / 100;
                ticket.Month = (ticketPrices[2].Price * pType.Coefficient) / 100;
                ticket.Year = (ticketPrices[3].Price * pType.Coefficient) / 100;

                return Ok(ticket);
            }
            else
            {
                Pricelist pricelist = DB.PricelistRepository.Find(p => p.From <= DateTime.Now && p.To >= DateTime.Now).FirstOrDefault();

                if (pricelist == null)
                {
                    ModelState.AddModelError("", "Pricelist is not defined for this period.");
                    return BadRequest(ModelState);
                }

                var ticketPrices = DB.TicketPriceRepository.Find(tp => tp.PricelistId == pricelist.Id).ToList();
                ticket.Hour = ticketPrices[0].Price; ;
                ticket.Day = ticketPrices[1].Price; ;
                ticket.Month = ticketPrices[2].Price; ;
                ticket.Year = ticketPrices[3].Price; ;

                return Ok(ticket);
            }
        }

        [HttpPost]
        [Route("BoughtTicket")]
        public IHttpActionResult BoughtTicket(BoughtTicketBindingModel ticket)
        {
            if (ticket.TicketType == "Hour")
            {
                DB.TicketRepository.Add(new Ticket() { PassengerId = DB.PassengerRepository.Find(p => p.UserName == ticket.Username).FirstOrDefault().Id, TicketTypeId = DB.TicketTypeRepository.Find(tt => tt.Type == ticket.TicketType).FirstOrDefault().Id, TotalPrice = ticket.Price, ExpirationDate = DateTime.Now.AddHours(1) });
            }
            else if (ticket.TicketType == "Day")
            {
                DB.TicketRepository.Add(new Ticket() { PassengerId = DB.PassengerRepository.Find(p => p.UserName == ticket.Username).FirstOrDefault().Id, TicketTypeId = DB.TicketTypeRepository.Find(tt => tt.Type == ticket.TicketType).FirstOrDefault().Id, TotalPrice = ticket.Price, ExpirationDate = DateTime.Now.AddDays(1).Date.Add(new TimeSpan(0, 0, 1)) });
            }
            else if (ticket.TicketType == "Month")
            {
                DB.TicketRepository.Add(new Ticket() { PassengerId = DB.PassengerRepository.Find(p => p.UserName == ticket.Username).FirstOrDefault().Id, TicketTypeId = DB.TicketTypeRepository.Find(tt => tt.Type == ticket.TicketType).FirstOrDefault().Id, TotalPrice = ticket.Price, ExpirationDate = DateTime.Now.AddMonths(1).Date.Add(new TimeSpan(0, 0, 1)) });
            }
            else if (ticket.TicketType == "Year")
            {
                DB.TicketRepository.Add(new Ticket() { PassengerId = DB.PassengerRepository.Find(p => p.UserName == ticket.Username).FirstOrDefault().Id, TicketTypeId = DB.TicketTypeRepository.Find(tt => tt.Type == ticket.TicketType).FirstOrDefault().Id, TotalPrice = ticket.Price, ExpirationDate = DateTime.Now.AddYears(1).Date.Add(new TimeSpan(0, 0, 1)) });
            }

            DB.Complete();
            return Ok();
        }

        // DELETE: api/Tickets/5
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult DeleteTicket(int id)
        {
            //Ticket ticket = db.Tickets.Find(id);
            //if (ticket == null)
            //{
            //    return NotFound();
            //}

            //db.Tickets.Remove(ticket);
            //db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TicketExists(int id)
        {
            return true;// db.Tickets.Count(e => e.Id == id) > 0;
        }
    }
}