using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp.Persistence.Repository
{
    public class TicketPriceRepository : Repository<TicketPriceRepository, int>, ITicketPriceRepository
    {
        public TicketPriceRepository(DbContext context) : base(context)
        { }
    }
}