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
        public IHttpActionResult adminloginvalid(Userlogin u)
        {
            try
            {
                var admin = db.admins.Where(x => x.username == u.email && x.password==u.password).FirstOrDefault();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (admin != null)
                {
                    bool isValid = true;
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
