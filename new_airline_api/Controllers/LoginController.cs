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
        [HttpPost]
        public IHttpActionResult loginvalid(Userlogin u)
        {
            try
            {
                var user = db.User_Master.Where(x => x.email_id == u.email).FirstOrDefault();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (user != null)
                {
                    bool isValid = BCrypt.Net.BCrypt.Verify(u.password, user.password);
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
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
    }
}
