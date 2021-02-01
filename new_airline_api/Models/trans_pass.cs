﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace new_airline_api.Models
{
    public class trans_pass
    {
        public int transaction_id { get; set; }
        public Nullable<int> user_Id { get; set; }
        public Nullable<int> flight_number { get; set; }
        public System.DateTime booking_date { get; set; }
        public int number_of_seats { get; set; }
        public System.DateTime travel_date { get; set; }
        public string seat_type { get; set; }
        public decimal amount { get; set; }
        public passenger[] Pass { get; set; }
    }
}