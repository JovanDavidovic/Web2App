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
    [RoutePrefix("api/DepartureTime")]
    public class DepartureTimesController : ApiController
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

        public DepartureTimesController(IUnitOfWork db)
        {
            this.DB = db;
        }

        // GET: api/DepartureTimes
        public IEnumerable<DepartureTime> GetDepartureTimes()
        {
            var ret = DB.DepartureTimeRepository.GetAll().ToList();
            return ret;
        }

        // GET: api/DepartureTimes/5
        [ResponseType(typeof(DepartureTime))]
        public IHttpActionResult GetDepartureTime(int id)
        {
            //DepartureTime departureTime = db.DepartureTimes.Find(id);
            //if (departureTime == null)
            //{
            //    return NotFound();
            //}

            return Ok();
        }

        // PUT: api/DepartureTimes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartureTime(int id, DepartureTime departureTime)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != departureTime.Id)
            //{
            //    return BadRequest();
            //}

            //db.Entry(departureTime).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!DepartureTimeExists(id))
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

        // POST: api/DepartureTimes
        [ResponseType(typeof(DepartureTime))]
        public IHttpActionResult PostDepartureTime(DepartureTime departureTime)
        {
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.DepartureTimes.Add(departureTime);
        //    db.SaveChanges();

            return Ok();//CreatedAtRoute("DefaultApi", new { id = departureTime.Id }, departureTime);
        }

        // DELETE: api/DepartureTimes/5
        [ResponseType(typeof(DepartureTime))]
        public IHttpActionResult DeleteDepartureTime(int id)
        {
            //DepartureTime departureTime = db.DepartureTimes.Find(id);
            //if (departureTime == null)
            //{
            //    return NotFound();
            //}

            //db.DepartureTimes.Remove(departureTime);
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

        private bool DepartureTimeExists(int id)
        {
            return true;
        }
    }
}