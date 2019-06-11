using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class DepartureTime
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public int DayTypeId { get; set; }
        public DayType DayT { get; set; }

        public string Routes { get; set; }
    }
}