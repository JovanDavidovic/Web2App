using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/TicketPrice")]
    public class TicketPricesController : ApiController
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

        public TicketPricesController(IUnitOfWork db)
        {
            this.DB = db;
        }

        // GET: api/TicketPrices
        public IEnumerable<PricelistBindingModel> GetTicketPrices()
        {
            List<PricelistBindingModel> ret = new List<PricelistBindingModel>();
            var pricelist = DB.PricelistRepository.GetAll().ToList();

            foreach (Pricelist p in pricelist)
            {
                var ticketPrices = DB.TicketPriceRepository.Find(tp => tp.PricelistId == p.Id).ToList();
                ret.Add(new PricelistBindingModel() { From = p.From.Date.ToString(), To = p.To.Date.ToString(), Hour = ticketPrices[0].Price, Day = ticketPrices[1].Price, Month = ticketPrices[2].Price, Year = ticketPrices[3].Price });
            }

            return ret;
        }

        // GET: api/TicketPrices/5
        [ResponseType(typeof(TicketPrice))]
        public IHttpActionResult GetTicketPrice(int id)
        {
            TicketPrice ticketPrice = DB.TicketPriceRepository.Get(id);
            if (ticketPrice == null)
            {
                return NotFound();
            }

            return Ok(ticketPrice);
        }

        // PUT: api/TicketPrices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTicketPrice(int id, TicketPrice ticketPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticketPrice.Id)
            {
                return BadRequest();
            }

            //db.Entry(ticketPrice).State = EntityState.Modified;

            try
            {
                //db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketPriceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TicketPrices
        [ResponseType(typeof(TicketPrice))]
        public IHttpActionResult PostTicketPrice(PricelistBindingModel pricelist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Pricelist plist = new Pricelist() { From = DateTime.ParseExact(pricelist.From, "yyyy-MM-dd", null), To = DateTime.ParseExact(pricelist.To, "yyyy-MM-dd", null) };
            DB.PricelistRepository.Add(plist);

            DB.TicketPriceRepository.Add(new TicketPrice() { Price = pricelist.Hour, PricelistId = plist.Id, TicketTypeId = 1 });
            DB.TicketPriceRepository.Add(new TicketPrice() { Price = pricelist.Day, PricelistId = plist.Id, TicketTypeId = 2 });
            DB.TicketPriceRepository.Add(new TicketPrice() { Price = pricelist.Month, PricelistId = plist.Id, TicketTypeId = 3 });
            DB.TicketPriceRepository.Add(new TicketPrice() { Price = pricelist.Year, PricelistId = plist.Id, TicketTypeId = 4 });

            DB.Complete();

            return Ok();
        }

        // DELETE: api/TicketPrices/5
        [ResponseType(typeof(TicketPrice))]
        public IHttpActionResult DeleteTicketPrice(int id)
        {
            //TicketPrice ticketPrice = db.TicketPrices.Find(id);
            //if (ticketPrice == null)
            //{
            //    return NotFound();
            //}

            //db.TicketPrices.Remove(ticketPrice);
            //db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TicketPriceExists(int id)
        {
            return true;// db.TicketPrices.Count(e => e.Id == id) > 0;
        }
    }
}