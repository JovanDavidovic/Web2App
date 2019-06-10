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
                ret.Add(new PricelistBindingModel() { From = p.From.ToShortDateString(), To = p.To.ToShortDateString(), Hour = ticketPrices[0].Price, Day = ticketPrices[1].Price, Month = ticketPrices[2].Price, Year = ticketPrices[3].Price });
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
            
            if ((plist.From >= plist.To) || (plist.From < DateTime.Now))
            {
                ModelState.AddModelError("", "Invalid time period for pricelist.");
                return BadRequest(ModelState);
            }

            var listOfPricelists = DB.PricelistRepository.GetAll().ToList();

            // provera poklapanja sa vec postojecim pricelist-ovima
            foreach (Pricelist p in listOfPricelists)
            {
                if(plist.From >= p.From && plist.From <= p.To)
                {
                    ModelState.AddModelError("", "Invalid time period for pricelist.");
                    return BadRequest(ModelState);
                }

                if(plist.To >= p.From && plist.To <= p.To)
                {
                    ModelState.AddModelError("", "Invalid time period for pricelist.");
                    return BadRequest(ModelState);
                }

                if(plist.From < p.From && plist.To > p.To)
                {
                    ModelState.AddModelError("", "Invalid time period for pricelist.");
                    return BadRequest(ModelState);
                }
            }

            DB.PricelistRepository.Add(plist);

            DB.TicketPriceRepository.Add(new TicketPrice() { Price = pricelist.Hour, PricelistId = plist.Id, TicketTypeId = 1 });
            DB.TicketPriceRepository.Add(new TicketPrice() { Price = pricelist.Day, PricelistId = plist.Id, TicketTypeId = 2 });
            DB.TicketPriceRepository.Add(new TicketPrice() { Price = pricelist.Month, PricelistId = plist.Id, TicketTypeId = 3 });
            DB.TicketPriceRepository.Add(new TicketPrice() { Price = pricelist.Year, PricelistId = plist.Id, TicketTypeId = 4 });

            DB.Complete();

            return Ok();
        }


        // POST: api/TicketPrices
        [HttpPost]
        [Route("ModifyPricelist")]
        [ResponseType(typeof(TicketPrice))]
        public IHttpActionResult ModifyPricelist(PricelistWithIdModel pricelist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            Pricelist plist = new Pricelist() { From = DateTime.ParseExact(pricelist.From, "yyyy-MM-dd", null), To = DateTime.ParseExact(pricelist.To, "yyyy-MM-dd", null) };
            Pricelist oldPlist = DB.PricelistRepository.Get(pricelist.Id);

            if ((plist.From >= plist.To) || (plist.From < DateTime.Now))
            {
                ModelState.AddModelError("", "Invalid time period for pricelist.");
                return BadRequest(ModelState);
            }

            var listOfPricelists = DB.PricelistRepository.GetAll().ToList();

            // provera poklapanja sa vec postojecim pricelist-ovima
            foreach (Pricelist p in listOfPricelists)
            {
                if(p.Id == oldPlist.Id)
                {
                    continue;
                }

                if (plist.From >= p.From && plist.From <= p.To)
                {
                    ModelState.AddModelError("", "Invalid time period for pricelist.");
                    return BadRequest(ModelState);
                }

                if (plist.To >= p.From && plist.To <= p.To)
                {
                    ModelState.AddModelError("", "Invalid time period for pricelist.");
                    return BadRequest(ModelState);
                }

                if (plist.From < p.From && plist.To > p.To)
                {
                    ModelState.AddModelError("", "Invalid time period for pricelist.");
                    return BadRequest(ModelState);
                }
            }

            var ticketPrices = DB.TicketPriceRepository.Find(tp => tp.PricelistId == oldPlist.Id).ToList();

            ticketPrices[0].Price = pricelist.Hour;
            ticketPrices[1].Price = pricelist.Day;
            ticketPrices[2].Price = pricelist.Month;
            ticketPrices[3].Price = pricelist.Year;

            foreach(TicketPrice tp in ticketPrices)
            {
                DB.TicketPriceRepository.Update(tp);
            }

            oldPlist.From = plist.From;
            oldPlist.To = plist.To;

            DB.PricelistRepository.Update(oldPlist);
            DB.Complete();

            return Ok();
        }


        [HttpGet]
        [Route("GetPricelist/{from}")]
        // GET: api/TicketPrices
        public IHttpActionResult GetPricelist(string from)
        {
            var pricelist = DB.PricelistRepository.GetAll().ToList();

            foreach (Pricelist p in pricelist)
            {
                if(p.From.ToShortDateString() == from)
                {
                    var ticketPrices = DB.TicketPriceRepository.Find(tp => tp.PricelistId == p.Id).ToList();
                    return Ok(new PricelistWithIdModel() { Id = p.Id, From = p.From.ToString("yyyy/MM/dd"), To = p.To.ToString("yyyy/MM/dd"), Hour = ticketPrices[0].Price, Day = ticketPrices[1].Price, Month = ticketPrices[2].Price, Year = ticketPrices[3].Price });
                }
            }

            ModelState.AddModelError("", "Cannot find pricelist.");
            return BadRequest(ModelState);
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

        [HttpGet]
        [Route("GetTicketPrices")]
        public IEnumerable<PricelistBindingModel> GetTicketPricesForModification()
        {
            List<PricelistBindingModel> ret = new List<PricelistBindingModel>();
            var pricelist = DB.PricelistRepository.GetAll().ToList();

            foreach (Pricelist p in pricelist)
            {
                if (p.From > DateTime.Now)
                {
                    var ticketPrices = DB.TicketPriceRepository.Find(tp => tp.PricelistId == p.Id).ToList();
                    ret.Add(new PricelistBindingModel() { From = p.From.ToShortDateString(), To = p.To.ToShortDateString(), Hour = ticketPrices[0].Price, Day = ticketPrices[1].Price, Month = ticketPrices[2].Price, Year = ticketPrices[3].Price });
                }
            }

            return ret;
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