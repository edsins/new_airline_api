using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class searchflightController : ApiController
    {
        private new_airlineEntities entity = new new_airlineEntities();
        [HttpGet]
        public IHttpActionResult searchflight(string departure, string arrival, string day, System.DateTime date, int seats)
        {
            List<new_airline_api.Models.sp_searchflight_Result> list = new List<new_airline_api.Models.sp_searchflight_Result>();
            var b = entity.sp_searchflight(departure, arrival, date, seats, day);
            foreach (var a in b)
            {
                if (a.sum == null || Convert.ToInt32(a.sum) < Convert.ToInt32(180 - seats))
                {
                    list.Add(a);
                }
            }
            return Ok(list);
        }
    }
}
