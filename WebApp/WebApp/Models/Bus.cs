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
        public float CoordinateX { get; set; }
        public float CoordinateY { get; set; }


        public int BusRouteId { get; set; }
        public Route BusRoute { get; set; }
    }
}