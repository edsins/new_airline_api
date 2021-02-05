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
    public class ResetPasswordController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();
        [HttpPost]
        public IHttpActionResult resetpassword(Userlogin u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = db.User_Master.Where(x => x.email_id == u.email).FirstOrDefault();
                if (user != null)
                {
                    var userobj = db.user_otp.Where(x => x.userid == user.userid).FirstOrDefault();
                    if(userobj==null)
                    {
                        return BadRequest("NOT ALLOWED");
                    }
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
                
                    return Ok("Password Changed");

                }
                else
                {
                    return BadRequest("Wrong Email");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
    }
}
