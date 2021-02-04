using new_airline_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace new_airline_api.Controllers
{
    public class UserController : ApiController
    {
        private new_airlineEntities entity = new new_airlineEntities();
        public HttpResponseMessage signup(User_Master user)
        {
            User_Master obj = entity.User_Master.Where(x => x.email_id == user.email_id).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (obj == null)
            { 
                user.password= BCrypt.Net.BCrypt.HashPassword(user.password);
                entity.User_Master.Add(user);
                try
                {
                    entity.SaveChanges();
                }
                catch
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not Found");
                }
                
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email-Id already Exists");
        }
    }
}
