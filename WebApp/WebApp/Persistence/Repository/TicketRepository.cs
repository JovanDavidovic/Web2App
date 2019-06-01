using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp.Persistence.Repository
{
    public class TicketRepository : Repository<TicketRepository, int>, ITicketRepository
    {
        public TicketRepository(DbContext context) : base(context)
        { }
    }
}