using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp.Persistence.Repository
{
    public class StationRepository : Repository<StationRepository, int>, IStationRepository
    {
        public StationRepository(DbContext context) : base(context)
        { }
    }
}