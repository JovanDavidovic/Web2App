using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
      
        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        [Dependency]
        public IBusRepository BusRepository { get; set; }

        [Dependency]
        public IDayTypeRepository DayTypeRepository { get; set; }

        [Dependency]
        public IDepartureTimeRepository DepartureTimeRepository { get; set; }

        [Dependency]
        public IPassengerRepository PassengerRepository { get; set; }

        [Dependency]
        public IPassengerTypeRepository PassengerTypeRepository { get; set; }

        [Dependency]
        public IPricelistRepository PricelistRepository { get; set; }

        [Dependency]
        public IRouteRepository RouteRepository { get; set; }

        [Dependency]
        public IStationRepository StationRepository { get; set; }

        [Dependency]
        public ITicketPriceRepository TicketPriceRepository { get; set; }

        [Dependency]
        public ITicketRepository TicketRepository { get; set; }

        [Dependency]
        public ITicketTypeRepository TicketTypeRepository { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}