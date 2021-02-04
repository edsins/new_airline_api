using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace new_airline_api.Models
{
    public class bookHistory
    {
        public int transaction_id { get; set; }
        public Nullable<int> flight_number { get; set; }
        public System.DateTime booking_date { get; set; }
        public int number_of_seats { get; set; }
        public System.DateTime travel_date { get; set; }
        public string seat_type { get; set; }
        public decimal amount { get; set; }
        public bool cancellation_status { get; set; }
        public string departure_location { get; set; }
        public string arrival_location { get; set; }
        public System.TimeSpan departure_time { get; set; }
        public System.TimeSpan arrival_time { get; set; }
        public System.TimeSpan duration { get; set; }
        public string [] seats { get; set; }

    }
}