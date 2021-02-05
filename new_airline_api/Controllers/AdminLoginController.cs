using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class AdminLoginController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();
        [HttpPost]
        public IHttpActionResult adminloginvalid(userlogin u)
        {
            var admin = db.admins.Where(x => x.username == u.email).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (admin != null)
            {
                bool isValid = BCrypt.Net.BCrypt.Verify(u.password, admin.password);
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
