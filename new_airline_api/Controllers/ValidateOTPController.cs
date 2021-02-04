using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class ValidateOTPController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();
        [HttpPost]
        public IHttpActionResult validateOTP(string email, int otp)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = db.User_Master.Where(x => x.email_id == email).FirstOrDefault();
            if(user==null)
            {
                return BadRequest("Email ID not Registered");
            }
            var userobj = db.user_otp.Where(x => x.userid == user.userid).FirstOrDefault();
            if(userobj.otp==otp)
            {
                return Ok("verified");
            }
            return BadRequest("OTP not Valid");
        }
    }
}
