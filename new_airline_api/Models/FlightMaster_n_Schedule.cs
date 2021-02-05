using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace new_airline_api.Models
{
    public class FlightMaster_n_Schedule
    {
        public int flight_number { get; set; }
        public string departure_location { get; set; }
        public string arrival_location { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public System.TimeSpan departure_time { get; set; }
        public System.TimeSpan arrival_time { get; set; }
        public System.TimeSpan duration { get; set; }    
    }
}