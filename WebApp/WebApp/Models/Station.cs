using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Station
    {
        [Key]
        public String Name { get; set; }
        public String Address { get; set; }
        public float CoordinatesX { get; set; }
        public float CoordinatesY { get; set; }
        public List<Route> Routes { get; set; }
    }
}