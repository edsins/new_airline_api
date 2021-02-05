using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class SearchFlightController : ApiController
    {
        private new_airlineEntities entity = new new_airlineEntities();
        [HttpGet]
        public IHttpActionResult searchflight(string departure, string arrival, string day, System.DateTime date, int seats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<new_airline_api.Models.sp_searchflight_Result> list_of_flights = new List<new_airline_api.Models.sp_searchflight_Result>();
                var searched_flights = entity.sp_searchflight(departure, arrival, date, seats, day);
                foreach (var flight in searched_flights)
                {
                    if (flight.sum == null || Convert.ToInt32(flight.sum) < Convert.ToInt32(120 - seats))
                    {
                        list_of_flights.Add(flight);
                    }
                }
                return Ok(list_of_flights);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
    }
}
