using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp.Persistence.Repository
{
    public class RouteRepository : Repository<RouteRepository, int>, IRouteRepository
    {
        public RouteRepository(DbContext context) : base(context)
        { }
    }
}