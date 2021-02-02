using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class SeatController : ApiController
    {
        private new_airlineEntities entity = new new_airlineEntities();
        [HttpGet]
        public IHttpActionResult getseats(int flightnumber, System.DateTime date)
        {
            return Ok(entity.sp_getseats(flightnumber, date));
        }
       
    }
}
