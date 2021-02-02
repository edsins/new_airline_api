using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class LoginController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();
        [HttpGet]
        public IHttpActionResult loginvalid(string email, string password)
        {
            var user = db.User_Master.Where(x => x.email_id == email).FirstOrDefault();

            if (user != null)
            {
                bool isValid = BCrypt.Net.BCrypt.Verify(password, user.password);
                if (!isValid)
                {
                    return BadRequest("Invalid Credentials");
                }

            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
            return Ok("Valid");
        }
    }
}
