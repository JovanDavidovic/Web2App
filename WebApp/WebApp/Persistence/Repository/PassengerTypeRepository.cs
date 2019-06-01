using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class PassengerTypeRepository : Repository<PassengerType, int>, IPassengerRepository
    {
        public PassengerTypeRepository(DbContext context) : base(context)
        {

        }
    }
}