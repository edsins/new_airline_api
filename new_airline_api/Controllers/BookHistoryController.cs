using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class BookHistoryController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();
        [HttpGet]
        public IHttpActionResult bookinghistory(string mail)
        {
            var user = db.User_Master.Where(x => x.email_id == mail).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            
            var bh = db.sp_booked_history(user.userid).ToArray();
            bookHistory [] histarray = new bookHistory[bh.Length];
            
            for(int i=0;i<bh.Length;i++)
            {
                var seatn=db.sp_seatsbooked(bh[i].transaction_id);
                var seats = seatn.ToArray();
                bookHistory b = new bookHistory();
                b.flight_number = bh[i].flight_number;
                b.transaction_id = bh[i].transaction_id;
                b.number_of_seats = bh[i].number_of_seats;
                b.travel_date = bh[i].travel_date;
                b.arrival_location = bh[i].arrival_location;
                b.departure_location = bh[i].departure_location;
                b.departure_time = bh[i].departure_time;
                b.arrival_time = bh[i].arrival_time;
                b.booking_date = bh[i].booking_date;
                b.cancellation_status = bh[i].cancellation_status;
                b.duration = bh[i].duration;
                b.amount = bh[i].amount;
                b.seat_type = bh[i].seat_type;
                b.seats = seats;
                histarray[i] = b;

            }

            return Ok(histarray);
        }
    }
}
