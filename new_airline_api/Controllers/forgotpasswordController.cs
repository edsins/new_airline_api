using Microsoft.AspNet.Identity;
using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class forgotpasswordController : ApiController
    {
        private new_airlineEntities db = new new_airlineEntities();
        public IHttpActionResult forgotpassword(Forgotpassword fp)
        {
            var user = db.User_Master.Where(x => x.email_id == fp.email).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (user!=null)
            {   
                if(user.security_question==fp.security_question && user.security_answer==fp.security_answer)
                {
                    return Ok("valid");
                }
                else
                {
                    return BadRequest("Incorrect Security Response");
                }
    
            }
            else
            {
                return BadRequest("Email Id does not exists");
            }
            
        }

    }
}
