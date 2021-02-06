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
    public class AdminOperationsController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();

        // GET: api/Flight_Master
        public IQueryable<FlightMaster_n_Schedule> GetFlight_Master()
        {
            List<Flight_Master> flightmaster = db.Flight_Master.ToList<Flight_Master>();
            List<FlightMaster_n_Schedule> allflights_with_schedule = new List<FlightMaster_n_Schedule>();
            for (int i = 0; i < flightmaster.Count(); i++)
            {
                FlightMaster_n_Schedule flight_with_schedule = new FlightMaster_n_Schedule();

                flight_with_schedule.flight_number = flightmaster[i].flight_number;
                flight_with_schedule.departure_location = flightmaster[i].departure_location;
                flight_with_schedule.arrival_location = flightmaster[i].arrival_location;
                flight_with_schedule.departure_time = flightmaster[i].departure_time;
                flight_with_schedule.arrival_time = flightmaster[i].arrival_time;
                flight_with_schedule.duration = flightmaster[i].duration;
                allflights_with_schedule.Add(flight_with_schedule);
            }
            var allflights = allflights_with_schedule.AsQueryable();
            return allflights;

        }


        [ResponseType(typeof(Flight_Master))]
        public IHttpActionResult GetFlight_Master(int id)
        {
            try
            {
                Flight_Master flight_Master = db.Flight_Master.Find(id);
                FlightMaster_n_Schedule flight_with_schedule = new FlightMaster_n_Schedule();
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
                        flight_with_schedule.Monday = true;
                    }
                    else if (wd.weekday == "Tuesday")
                    {
                        flight_with_schedule.Tuesday = true;
                    }
                    else if (wd.weekday == "Wednesday")
                    {
                        flight_with_schedule.Wednesday = true;
                    }
                    else if (wd.weekday == "Thursday")
                    {
                        flight_with_schedule.Thursday = true;
                    }
                    else if (wd.weekday == "Friday")
                    {
                        flight_with_schedule.Friday = true;
                    }
                    else if (wd.weekday == "Saturday")
                    {
                        flight_with_schedule.Saturday = true;
                    }
                    else if (wd.weekday == "Sunday")
                    {
                        flight_with_schedule.Sunday = true;
                    }
                }

                flight_with_schedule.flight_number = flight_Master.flight_number;
                flight_with_schedule.departure_location = flight_Master.departure_location;
                flight_with_schedule.arrival_location = flight_Master.arrival_location;
                flight_with_schedule.departure_time = flight_Master.departure_time;
                flight_with_schedule.arrival_time = flight_Master.arrival_time;
                flight_with_schedule.duration = flight_Master.duration;
                var Flight_cost = db.flight_cost.Where(x => x.flight_number == id).FirstOrDefault();
                if(Flight_cost!=null)
                {
                    flight_with_schedule.economy_cost = Flight_cost.economy_price;
                    flight_with_schedule.business_cost = Flight_cost.business_price;
                    
                }
                return Ok(flight_with_schedule);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            
        }

        // PUT: api/Flight_Master/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFlight_Master(int id, FlightMaster_n_Schedule flight_with_schedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flight_with_schedule.flight_number)
            {
                return BadRequest();
            }
            try
            {
                Flight_Master flight_Master = new Flight_Master();
                flight_Master.flight_number = flight_with_schedule.flight_number;
                flight_Master.departure_location = flight_with_schedule.departure_location;
                flight_Master.arrival_location = flight_with_schedule.arrival_location;
                flight_Master.departure_time = flight_with_schedule.departure_time;
                flight_Master.arrival_time = flight_with_schedule.arrival_time;
                flight_Master.duration = flight_with_schedule.duration;

                var Flight_cost = db.flight_cost.Where(x => x.flight_number == flight_with_schedule.flight_number).FirstOrDefault();
                if(Flight_cost!=null)
                {
                    Flight_cost.economy_price = flight_with_schedule.economy_cost;
                    Flight_cost.business_price = flight_with_schedule.business_cost;
                    db.Entry(Flight_cost).State = EntityState.Modified;
                }
                else
                {
                    Flight_cost.economy_price = flight_with_schedule.economy_cost;
                    Flight_cost.business_price = flight_with_schedule.business_cost;
                    db.flight_cost.Add(Flight_cost);
                }
                
                var weekdata = from Flight_schedule in db.flight_schedule
                               where Flight_schedule.flight_number == id
                               select Flight_schedule;
                foreach (var wd in weekdata)
                {
                    db.flight_schedule.Remove(wd);
                }
                List<string> day = new List<string>();

                if (flight_with_schedule.Monday == true)
                {
                    day.Add("Monday");
                }
                if (flight_with_schedule.Tuesday == true)
                {
                    day.Add("Tuesday");
                }
                if (flight_with_schedule.Wednesday == true)
                {
                    day.Add("Wednesday");
                }
                if (flight_with_schedule.Thursday == true)
                {
                    day.Add("Thursday");
                }
                if (flight_with_schedule.Friday == true)
                {
                    day.Add("Friday");
                }
                if (flight_with_schedule.Saturday == true)
                {
                    day.Add("Saturday");
                }
                if (flight_with_schedule.Sunday == true)
                {
                    day.Add("Sunday");
                }
                for (int i = 0; i < day.Count; i++)
                {
                    db.sp_Addflightschedule(flight_with_schedule.flight_number, day[i]);
                }
                db.Entry(flight_Master).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!Flight_MasterExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest(ex.ToString());
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }  
        }

        // POST: api/Flight_Master
        [ResponseType(typeof(Flight_Master))]
        public IHttpActionResult PostFlight_Master(FlightMaster_n_Schedule addflight)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                flight_cost Flight_cost = new flight_cost();
                Flight_Master flight_Master = new Flight_Master();
                flight_schedule flight_Schedule = new flight_schedule();
                flight_Master.flight_number = addflight.flight_number;
                flight_Master.departure_location = addflight.departure_location;
                flight_Master.arrival_location = addflight.arrival_location;
                flight_Master.departure_time = addflight.departure_time;
                flight_Master.arrival_time = addflight.arrival_time;
                flight_Master.duration = addflight.duration;

                db.Flight_Master.Add(flight_Master);
                Flight_cost.flight_number = addflight.flight_number;
                Flight_cost.economy_price = addflight.economy_cost;
                Flight_cost.business_price = addflight.business_cost;
                db.flight_cost.Add(Flight_cost);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    if (Flight_MasterExists(flight_Master.flight_number))
                    {
                        return Conflict();
                    }
                    else
                    {
                        return BadRequest(ex.ToString());
                    }
                }
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
                catch (DbUpdateException ex)
                {
                    if (Flight_MasterExists(flight_Master.flight_number))
                    {
                        return Conflict();
                    }
                    else
                    {
                        return BadRequest(ex.ToString());
                    }
                }

                return CreatedAtRoute("DefaultApi", new { id = flight_Master.flight_number }, flight_Master);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            
        }

        // DELETE: api/Flight_Master/5
        [ResponseType(typeof(Flight_Master))]
        public IHttpActionResult DeleteFlight_Master(int id)
        {
            try 
            {
                Flight_Master flight_Master = db.Flight_Master.Find(id);
                if (flight_Master == null)
                {
                    return NotFound();
                }
                var flight = db.flight_cost.Where(x => x.flight_number == id).FirstOrDefault();
                if(flight!=null)
                {
                    db.flight_cost.Remove(flight);
                }
               
                var weekdata = from Flight_Schedule in db.flight_schedule
                               where Flight_Schedule.flight_number == id
                               select Flight_Schedule;
                foreach (var wd in weekdata)
                {
                    db.flight_schedule.Remove(wd);
                }
                db.Flight_Master.Remove(flight_Master);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    if (Flight_MasterExists(flight_Master.flight_number))
                    {
                        return Conflict();
                    }
                    else
                    {
                        return BadRequest(ex.ToString());
                    }
                }

                return Ok(flight_Master);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            
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