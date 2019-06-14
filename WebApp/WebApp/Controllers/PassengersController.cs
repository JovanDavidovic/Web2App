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
using System.Net.Mail;
using System.Web;
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
        [HttpGet]
        public IEnumerable<Passenger> GetUsers()
        {
            var ret = DB.PassengerRepository.Find(p => p.VerificationStatus == "PROCESSING").ToList();
            return ret;
        }

        // GET: api/Passengers/5
        [ResponseType(typeof(Passenger))]
        public IHttpActionResult GetPassenger(string id)
        {
            Passenger passenger = DB.PassengerRepository.Get(id);
            if (passenger == null)
            {
                passenger = DB.PassengerRepository.Find(p => p.UserName == id).FirstOrDefault();
                if (passenger == null)
                {
                    return NotFound();
                }
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
        [AllowAnonymous]
        [ResponseType(typeof(Passenger))]
        public IHttpActionResult PostPassenger(Passenger passenger)
        {
            Passenger returnPassenger = DB.PassengerRepository.Get(passenger.Email);

            if (returnPassenger == null)
            {
                returnPassenger = DB.PassengerRepository.Find(p => p.UserName == passenger.UserName).FirstOrDefault();
                if (returnPassenger == null)
                {
                    ModelState.AddModelError("", "User not found!");
                    return BadRequest(ModelState);
                }
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

        [HttpPost]
        [Route("Deny")]
        public IHttpActionResult Deny(Passenger passenger)
        {
            Passenger deniedPassenger = DB.PassengerRepository.Get(passenger.Email);

            if (deniedPassenger == null)
            {
                deniedPassenger = DB.PassengerRepository.Find(p => p.UserName == passenger.UserName).FirstOrDefault();
                if (deniedPassenger == null)
                {
                    ModelState.AddModelError("", "User not found!");
                    return BadRequest(ModelState);
                }
            }

            deniedPassenger.VerificationStatus = "DENIED";
            DB.PassengerRepository.Update(deniedPassenger);
            DB.Complete();

            return Ok();
        }

        [HttpPost]
        [Route("Accept")]
        public IHttpActionResult Accept(Passenger passenger)
        {
            Passenger deniedPassenger = DB.PassengerRepository.Get(passenger.Email);

            if (deniedPassenger == null)
            {
                deniedPassenger = DB.PassengerRepository.Find(p => p.UserName == passenger.UserName).FirstOrDefault();
                if (deniedPassenger == null)
                {
                    ModelState.AddModelError("", "User not found!");
                    return BadRequest(ModelState);
                }
            }

            deniedPassenger.VerificationStatus = "ACCEPTED";
            DB.PassengerRepository.Update(deniedPassenger);
            DB.Complete();

            MailMessage mail = new MailMessage("bid.incorporated.ns@gmail.com", passenger.Email);
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential("bid.incorporated.ns@gmail.com", "B1i2d3i4n5c6");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            mail.Subject = "Ticket information";
            mail.Body = $"";
            try
            {
                client.Send(mail);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("GetPhoto")]
        public IHttpActionResult GetPhoto(Passenger passenger)
        {
            Passenger picturePassenger = DB.PassengerRepository.Get(passenger.Email);

            if (picturePassenger == null)
            {
                picturePassenger = DB.PassengerRepository.Find(p => p.UserName == passenger.UserName).FirstOrDefault();
                if (picturePassenger == null)
                {
                    ModelState.AddModelError("", "User not found!");
                    return BadRequest(ModelState);
                }
            }

            if(picturePassenger.Image == null)
            {
                ModelState.AddModelError("", "Photo does not exist!");
                return BadRequest(ModelState);
            }

            var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + picturePassenger.Image);

            FileInfo fileInfo = new FileInfo(filePath);
            string type = fileInfo.Extension.Split('.')[1];
            byte[] data = new byte[fileInfo.Length];

            HttpResponseMessage response = new HttpResponseMessage();
            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ByteArrayContent(data);
                response.Content.Headers.ContentLength = data.Length;

            }

            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/png");

            return Ok(data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PassengerExists(string id)
        {
            return true;// db.Users.Count(e => e.Id == id) > 0;
        }
    }
}