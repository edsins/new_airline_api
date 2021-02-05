using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class CancelHistoryController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();
        [HttpGet]
        public IHttpActionResult Cancellationhistory(string mail)
        {
            try
            {
                var user = db.User_Master.Where(x => x.email_id == mail).FirstOrDefault();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (user == null)
                {
                    return BadRequest("User Not Found");
                }
                return Ok(db.sp_cancel_booked(user.userid));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
