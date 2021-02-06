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
        public IHttpActionResult PostTransaction(Transaction_n_Passenger trans_pass)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Transaction transaction = new Transaction();
               
                credit_card card = new credit_card();
                var user = db.User_Master.Where(x => x.email_id == trans_pass.email).FirstOrDefault();
                if (user == null)
                {
                    return BadRequest("User does not Exists");
                }

                transaction.flight_number = trans_pass.flight_number;
                transaction.booking_date = trans_pass.booking_date;
                transaction.number_of_seats = trans_pass.number_of_seats;
                transaction.seat_type = trans_pass.seat_type;
                transaction.travel_date = trans_pass.travel_date;
                transaction.amount = trans_pass.amount;
                transaction.user_Id = user.userid;
                db.Transactions.Add(transaction);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }

                card = trans_pass.carddetails;
                card.userid = user.userid;
                db.credit_card.Add(card);
                
                var last_trans = db.Transactions.ToList().Last();
                for (int i = 0; i < trans_pass.passengers.Length; i++)
                {
                    passenger Passenger_obj = new passenger();
                    Passenger_obj.transaction_id = last_trans.transaction_id;
                    Passenger_obj.email = trans_pass.contact_email;
                    Passenger_obj.contact = trans_pass.contact_no;
                    Passenger_obj.seatno = trans_pass.seatarray[i];
                    Passenger_obj.Name = trans_pass.passengers[i].firstname;
                    Passenger_obj.age = trans_pass.passengers[i].age;
                    db.passengers.Add(Passenger_obj); 
                }
             

                return CreatedAtRoute("DefaultApi", new { id = transaction.transaction_id }, transaction);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            
        }
    }
}
