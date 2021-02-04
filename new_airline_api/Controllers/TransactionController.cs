using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class TransactionController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();
        public IHttpActionResult PostTransaction(trans_pass tp)
        {
            Transaction transaction = new Transaction();
            passenger Passenger_obj = new passenger();
            credit_card card = new credit_card();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = db.User_Master.Where(x => x.email_id == tp.email).FirstOrDefault();
            if(user==null)
            {
                return BadRequest("User does not Exists");
            }
           
            transaction.flight_number = tp.flight_number;
            transaction.booking_date = tp.booking_date;
            transaction.number_of_seats = tp.number_of_seats;
            transaction.seat_type = tp.seat_type;
            transaction.travel_date = tp.travel_date;
            transaction.amount = tp.amount;
            transaction.user_Id = user.userid;
            db.Transactions.Add(transaction);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return NotFound();
            }
            card = tp.carddetails;
            card.userid = user.userid;
            db.credit_card.Add(card);
            var last_trans = db.Transactions.ToList().Last();
            for (int i = 0; i < tp.passengers.Length; i++)
            {
                Passenger_obj.transaction_id = last_trans.transaction_id;
                Passenger_obj.email = tp.contact_email;
                Passenger_obj.contact = tp.contact_no;
                Passenger_obj.seatno = tp.seatarray[i];
                Passenger_obj.Name = tp.passengers[i].firstname;
                Passenger_obj.age = tp.passengers[i].age;
                db.passengers.Add(Passenger_obj);
            }
            try
            { 
                db.SaveChanges();
            }
            catch
            {
                return NotFound();
            }
           

            return CreatedAtRoute("DefaultApi", new { id = transaction.transaction_id }, transaction);
        }
    }
}
