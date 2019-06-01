using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // IStanicaRepository StanicaRepository { get; set; }

        IBusRepository BusRepository { get; set; }
        IDayTypeRepository DayTypeRepository { get; set; }
        IDepartureTimeRepository DepartureTimeRepository { get; set; }
        IPassengerRepository PassengerRepository { get; set; }
        IPassengerTypeRepository PassengerTypeRepository { get; set; }
        IPricelistRepository PricelistRepository { get; set; }
        IRouteRepository RouteRepository { get; set; }
        IStationRepository StationRepository { get; set; }
        ITicketPriceRepository TicketPriceRepository { get; set; }
        ITicketRepository TicketRepository { get; set; }
        ITicketTypeRepository TicketTypeRepository { get; set; }


        int Complete();
    }
}
