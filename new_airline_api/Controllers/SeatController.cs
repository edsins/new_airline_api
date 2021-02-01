﻿using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class SeatController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();
        public IHttpActionResult PostTransaction(trans_pass tp)
        {
            Transaction transaction = new Transaction();
            passenger Passen = new passenger();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            transaction.flight_number = tp.flight_number;
            transaction.booking_date = tp.booking_date;
            transaction.number_of_seats = tp.number_of_seats;
            transaction.seat_type = tp.seat_type;
            transaction.travel_date = tp.travel_date;
            transaction.amount = tp.amount;
            transaction.user_Id = tp.user_Id;
            db.Transactions.Add(transaction);
            db.SaveChanges();
       
            for (int i = 0; i < tp.Pass.Length; i++)
            {
                var last_trans = db.Transactions.ToList().Last();
                
                Passen = tp.Pass[i];
                Passen.transaction_id = last_trans.transaction_id;
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