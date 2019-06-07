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
    [RoutePrefix("api/Passenger")]
    public class PassengersController : ApiController
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

        public PassengersController(IUnitOfWork db)
        {
            this.DB = db;
        }

        // GET: api/Passengers
        public IQueryable<Passenger> GetUsers()
        {
            return DB.PassengerRepository.Find(p => p.VerificationStatus == "PROCESSING").AsQueryable();
        }

        // GET: api/Passengers/5
        [ResponseType(typeof(Passenger))]
        public IHttpActionResult GetPassenger(string id)
        {
            Passenger passenger = DB.PassengerRepository.Get(id);
            if (passenger == null)
            {
                return NotFound();
            }

            return Ok(passenger);
        }

        // PUT: api/Passengers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPassenger(string id, RegisterBindingModel passenger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Passenger oldPassenger = DB.PassengerRepository.Get(passenger.Email);

            if (oldPassenger != null)
            {
                oldPassenger.Name = passenger.Name;
                oldPassenger.LastName = passenger.Lastname;
                oldPassenger.Address = passenger.Address;

                if (oldPassenger.UserName != passenger.Username)
                {
                    if (DB.PassengerRepository.Find(p => p.UserName == passenger.Username).FirstOrDefault() != null)
                    {
                        ModelState.AddModelError("", "User with the same username already exists");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        oldPassenger.UserName = passenger.Username;
                    }
                }

                PassengerType rowInPassengerTypeTable = DB.PassengerTypeRepository.Find(g => g.Type == passenger.Acctype).FirstOrDefault();

                if (oldPassenger.TypeId == 1 && rowInPassengerTypeTable.Id != 1)
                {
                    oldPassenger.VerificationStatus = "PROCESSING";
                }
                else if(oldPassenger.TypeId != 1 && rowInPassengerTypeTable.Id != 1 && oldPassenger.TypeId != rowInPassengerTypeTable.Id)
                {
                    oldPassenger.VerificationStatus = "PROCESSING";
                }

                oldPassenger.TypeId = rowInPassengerTypeTable.Id;

                oldPassenger.DateOfBirth = passenger.Birthday;
                oldPassenger.Address = passenger.Address;

                if (oldPassenger.PasswordHash != ApplicationUser.HashPassword(passenger.Password))
                {
                    oldPassenger.PasswordHash = ApplicationUser.HashPassword(passenger.Password);
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found!");
                return BadRequest(ModelState);
            }

            try
            {
                DB.PassengerRepository.Update(oldPassenger);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            DB.Complete();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Passengers
        [ResponseType(typeof(Passenger))]
        public IHttpActionResult PostPassenger(Passenger passenger)
        {
            Passenger returnPassenger = DB.PassengerRepository.Get(passenger.Email);

            if (returnPassenger == null)
            {
                ModelState.AddModelError("", "User not found!");
                return BadRequest(ModelState);
            }

            return Ok(returnPassenger);
        }

        // DELETE: api/Passengers/5
        [ResponseType(typeof(Passenger))]
        public IHttpActionResult DeletePassenger(string id)
        {
            //Passenger passenger = db.Users.Find(id);
            //if (passenger == null)
            //{
            //    return NotFound();
            //}

            //db.Users.Remove(passenger);
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

        private bool PassengerExists(string id)
        {
            return true;// db.Users.Count(e => e.Id == id) > 0;
        }
    }
}