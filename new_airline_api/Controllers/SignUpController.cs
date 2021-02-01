using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class SignUpController : ApiController
    {
        private new_airlineEntities entity = new new_airlineEntities();
        public HttpResponseMessage signup(User_Master user)
        {
            User_Master obj = entity.User_Master.Where(x => x.email_id == user.email_id).FirstOrDefault();
            if (obj == null)
            {
                entity.User_Master.Add(user);
                entity.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, user);

            }


            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email-Id already Exists");
        }
    }
}
