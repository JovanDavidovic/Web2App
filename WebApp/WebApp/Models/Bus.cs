using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Bus
    {
        [Key]
        public int Number { get; set; }
        public Tuple<float, float> Coordinates { get; set; }


        public int BusRouteId { get; set; }
        public Route BusRoute { get; set; }
    }
}