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

        [HttpPost]
        [Route("AddRoute")]
        public IHttpActionResult AddRoute(RouteBindingModel route)
        {
            var stations = route.RouteStations.Split('-');
            Route newRoute = new Route() { Number = route.Name };

            var random = new Random();

            for (int i = 1; i < stations.Count(); i++)
            {
                var station = new Station() { Name = "Station" + random.Next(1, 9999).ToString(), CoordinatesX = float.Parse(stations[i].Split(':')[0]), CoordinatesY = float.Parse(stations[i].Split(':')[1]), Address = "Address" };
                DB.StationRepository.Add(station);
                if (newRoute.Stations == null)
                {
                    newRoute.Stations = ":" + station.Name;
                }
                else
                {
                    newRoute.Stations += ":" + station.Name;
                }
            }

            DB.RouteRepository.Add(newRoute);

            DB.Complete();

            return Ok();
        }

        [HttpGet]
        [Route("GetRoutes")]
        public IHttpActionResult GetRoutes()
        {
            List<RouteBindingModel> rbm = new List<RouteBindingModel>();
            var routes = DB.RouteRepository.GetAll().ToList();
            foreach (var rt in routes)
            {
                rbm.Add(new RouteBindingModel() { Name = rt.Number, RouteStations = rt.Stations });
            }
            return Ok(rbm);
        }

        [HttpGet]
        [Route("GetRoute/{id}")]
        public IHttpActionResult GetRoute(int id)
        {
            Route route = DB.RouteRepository.Find(r => r.Number == id).FirstOrDefault();

            var stations = route.Stations.Split(':');

            RouteBindingModel rbm = new RouteBindingModel() { Name = route.Number, RouteStations = ""};

            for(int i=1; i<stations.Count(); i++)
            {
                var st = DB.StationRepository.Find(s => s.Name == stations[i]).FirstOrDefault();
                rbm.RouteStations = "-" + st.CoordinatesX.ToString() + ":" + st.CoordinatesY.ToString();
            }

            return Ok(rbm);
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