using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Route
    {
        [Key]
        public int Number { get; set; }
        public List<Station> Stations { get; set; }
        public List<Bus> Buses { get; set; }

        public int DepartureTimeId { get; set; }
        public DepartureTime DepartureTime { get; set; }

        public int DayTypeId { get; set; }
        public DayType DayType { get; set; }
    }
}