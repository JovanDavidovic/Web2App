using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp.Persistence.Repository
{
    public class TicketTypeRepository : Repository<TicketTypeRepository, int>, ITicketTypeRepository
    {
        public TicketTypeRepository(DbContext context) : base(context)
        { }
    }
}