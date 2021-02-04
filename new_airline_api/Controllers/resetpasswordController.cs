using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class resetpasswordController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();
        public IHttpActionResult resetpassword(userlogin u)
        {
            var user = db.User_Master.Where(x => x.email_id == u.email).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (user != null)
            {
                user.password = BCrypt.Net.BCrypt.HashPassword(u.password);
                db.Entry(user).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return NotFound();
                }
                
                return Ok(user);

            }
            else
            {
                return BadRequest("Wrong Email");
            }

        }
    }
}
