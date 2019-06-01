using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class DepartureTimeRepository : Repository<DepartureTime, int>, IDepartureTimeRepository
    {
        public DepartureTimeRepository(DbContext context) : base(context)
        {

        }
    }
}