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
        public string Stations { get; set; }
    }
}