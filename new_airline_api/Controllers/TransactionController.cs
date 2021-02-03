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
            passenger Passen = new passenger();
            credit_card card = new credit_card();
            
            var user = db.User_Master.Where(x => x.email_id == tp.email).FirstOrDefault();
            transaction.flight_number = tp.flight_number;
            transaction.booking_date = tp.booking_date;
            transaction.number_of_seats = tp.number_of_seats;
            transaction.seat_type = tp.seat_type;
            transaction.travel_date = tp.travel_date;
            transaction.amount = tp.amount;
            transaction.user_Id = user.userid;
            db.Transactions.Add(transaction);
            db.SaveChanges();
            card= tp.carddetails;
            card.userid = user.userid;
            db.credit_card.Add(card);
            var last_trans = db.Transactions.ToList().Last();
            for (int i = 0; i < tp.passengers.Length; i++)
            {
                Passen = tp.passengers[i];
                Passen.transaction_id = last_trans.transaction_id;
                Passen.email = tp.email;
                Passen.seatno = tp.seatarray[i];
                db.passengers.Add(Passen);
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return CreatedAtRoute("DefaultApi", new { id = transaction.transaction_id }, transaction);
        }
    }
}
