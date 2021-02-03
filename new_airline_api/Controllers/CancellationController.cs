﻿using new_airline_api.Models;
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
    public class CancellationController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();
        public IHttpActionResult Cancelreservation(int transid, DateTime cancel_date, decimal cancel_amount)
        {
            var trans = db.Transactions.Where(x => x.transaction_id == transid).FirstOrDefault();
            if (trans != null)
            {
                trans.cancellation_status = true;
                db.Entry(trans).State = EntityState.Modified;
                var pass = from p in db.passengers
                           where p.transaction_id == transid
                           select p;
                foreach (var p in pass)
                {
                    db.passengers.Remove(p);
                }
                cancellation can = new cancellation();
                can.transaction_id = transid;
                can.cancellation_date = cancel_date;
                can.cancellation_amount = cancel_amount;
                db.cancellations.Add(can);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw;
                }
                return Ok(can);
            }
            else
            {
                return BadRequest("Transaction Id does not exist");

            }
        }
    }
}
