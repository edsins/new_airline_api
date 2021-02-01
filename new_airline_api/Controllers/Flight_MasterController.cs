using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using new_airline_api.Models;

namespace new_airline_api.Controllers
{
    public class Flight_MasterController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();

        // GET: api/Flight_Master
        public IQueryable<flight_master_schedule> GetFlight_Master()
        {
            List<Flight_Master> flightmaster = db.Flight_Master.ToList<Flight_Master>();
            List<flight_master_schedule> allflightswschedule = new List<flight_master_schedule>();
            for (int i = 0; i < flightmaster.Count(); i++)
            {
                flight_master_schedule flightwsche = new flight_master_schedule();
                
                flightwsche.flight_number = flightmaster[i].flight_number;
                flightwsche.departure_location = flightmaster[i].departure_location;
                flightwsche.arrival_location = flightmaster[i].arrival_location;
                flightwsche.departure_time = flightmaster[i].departure_time;
                flightwsche.arrival_time = flightmaster[i].arrival_time;
                flightwsche.duration = flightmaster[i].duration;
                allflightswschedule.Add(flightwsche);
            }
            var allflight = allflightswschedule.AsQueryable();
                return allflight;
        }

        // GET: api/Flight_Master/5
        [ResponseType(typeof(Flight_Master))]
        public IHttpActionResult GetFlight_Master(int id)
        {
            Flight_Master flight_Master = db.Flight_Master.Find(id);
            flight_master_schedule flightwsche = new flight_master_schedule();
            if (flight_Master == null)
            {
                return NotFound();
            }
            var weekdata = from fs in db.flight_schedule
                           where fs.flight_number == id
                           select fs;
            foreach (var wd in weekdata)
            {
                if (wd.weekday == "Monday")
                {
                    flightwsche.Monday = true;
                }
                else if (wd.weekday == "Tuesday")
                {
                    flightwsche.Tuesday = true;
                }
                else if (wd.weekday == "Wednesday")
                {
                    flightwsche.Wednesday = true;
                }
                else if (wd.weekday == "Thursday")
                {
                    flightwsche.Thursday = true;
                }
                else if (wd.weekday == "Friday")
                {
                    flightwsche.Friday = true;
                }
                else if (wd.weekday == "Saturday")
                {
                    flightwsche.Saturday = true;
                }
                else if (wd.weekday == "Sunday")
                {
                    flightwsche.Sunday = true;
                }
            }

            flightwsche.flight_number = flight_Master.flight_number;
            flightwsche.departure_location = flight_Master.departure_location;
            flightwsche.arrival_location = flight_Master.arrival_location;
            flightwsche.departure_time = flight_Master.departure_time;
            flightwsche.arrival_time = flight_Master.arrival_time;
            flightwsche.duration = flight_Master.duration;

            return Ok(flightwsche);
        }

        // PUT: api/Flight_Master/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFlight_Master(int id, flight_master_schedule flight_w_sche)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flight_w_sche.flight_number)
            {
                return BadRequest();
            }

            Flight_Master flight_Master = new Flight_Master();
            flight_Master.flight_number = flight_w_sche.flight_number;
            flight_Master.departure_location = flight_w_sche.departure_location;
            flight_Master.arrival_location = flight_w_sche.arrival_location;
            flight_Master.departure_time = flight_w_sche.departure_time;
            flight_Master.arrival_time = flight_w_sche.arrival_time;
            flight_Master.duration = flight_w_sche.duration;

            var weekdata = from fs in db.flight_schedule
                           where fs.flight_number == id
                           select fs;
            foreach (var wd in weekdata)
            {
                db.flight_schedule.Remove(wd);
            }
            List<string> day = new List<string>();

            if (flight_w_sche.Monday == true)
            {
                day.Add("Monday");
            }
            if (flight_w_sche.Tuesday == true)
            {
                day.Add("Tuesday");
            }
            if (flight_w_sche.Wednesday == true)
            {
                day.Add("Wednesday");
            }
            if (flight_w_sche.Thursday == true)
            {
                day.Add("Thursday");
            }
            if (flight_w_sche.Friday == true)
            {
                day.Add("Friday");
            }
            if (flight_w_sche.Saturday == true)
            {
                day.Add("Saturday");
            }
            if (flight_w_sche.Sunday == true)
            {
                day.Add("Sunday");
            }
            for (int i = 0; i < day.Count; i++)
            {
                db.sp_Addflightschedule(flight_w_sche.flight_number, day[i]);
            }
            db.Entry(flight_Master).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Flight_MasterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Flight_Master
        [ResponseType(typeof(Flight_Master))]
        public IHttpActionResult PostFlight_Master(flight_master_schedule addflight)
        {
            Flight_Master flight_Master = new Flight_Master();
            flight_schedule flight_Schedule = new flight_schedule();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            flight_Master.flight_number = addflight.flight_number;
            flight_Master.departure_location = addflight.departure_location;
            flight_Master.arrival_location = addflight.arrival_location;
            flight_Master.departure_time = addflight.departure_time;
            flight_Master.arrival_time = addflight.arrival_time;
            flight_Master.duration = addflight.duration;
            db.Flight_Master.Add(flight_Master);
            db.SaveChanges();
            List<string> day = new List<string>();

            if (addflight.Monday == true)
            {
                day.Add("Monday");
            }
            if (addflight.Tuesday == true)
            {
                day.Add("Tuesday");
            }
            if (addflight.Wednesday == true)
            {
                day.Add("Wednesday");
            }
            if (addflight.Thursday == true)
            {
                day.Add("Thursday");
            }
            if (addflight.Friday == true)
            {
                day.Add("Friday");
            }
            if (addflight.Saturday == true)
            {
                day.Add("Saturday");
            }
            if (addflight.Sunday == true)
            {
                day.Add("Sunday");
            }
            for (int i = 0; i < day.Count; i++)
            {
                db.sp_Addflightschedule(addflight.flight_number, day[i]);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Flight_MasterExists(flight_Master.flight_number))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = flight_Master.flight_number }, flight_Master);
        }

        // DELETE: api/Flight_Master/5
        [ResponseType(typeof(Flight_Master))]
        public IHttpActionResult DeleteFlight_Master(int id)
        {
            Flight_Master flight_Master = db.Flight_Master.Find(id);
            if (flight_Master == null)
            {
                return NotFound();
            }
            var weekdata = from fs in db.flight_schedule
                           where fs.flight_number == id
                           select fs;
            foreach (var wd in weekdata)
            {
                db.flight_schedule.Remove(wd);
            }
            db.Flight_Master.Remove(flight_Master);
            db.SaveChanges();

            return Ok(flight_Master);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Flight_MasterExists(int id)
        {
            return db.Flight_Master.Count(e => e.flight_number == id) > 0;
        }
    }
}