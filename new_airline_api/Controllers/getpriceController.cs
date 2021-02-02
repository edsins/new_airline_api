using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class getpriceController : ApiController
    {
        private new_airlineEntities entity = new new_airlineEntities();
        [HttpGet]
        public IHttpActionResult getprice(int flightnumber, String seatclass)
        {
            var flight = entity.flight_cost.Where(x => x.flight_number == flightnumber).FirstOrDefault();
            if (seatclass.Contains("business"))
                return Ok(flight.business_price);
            else
                return Ok(flight.economy_price);

        }
    }
}
