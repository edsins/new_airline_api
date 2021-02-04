using Microsoft.AspNet.Identity;
using MimeKit;
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
        [HttpPost]
        public IHttpActionResult forgotpassword(Forgotpassword fp)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                Random r = new Random();
                int OTP = r.Next(1000, 9999);
                var user = db.User_Master.Where(x => x.email_id == fp.email).FirstOrDefault();
                if(user==null)
                {
                    return BadRequest("user doesnot exist");
                }
                
                var u = db.user_otp.Where(x => x.userid == user.userid).FirstOrDefault();
                if(u!=null)
                { 
                    db.user_otp.Remove(u);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.ToString());
                    }

                }
               
                user_otp userobj = new user_otp();
                userobj.userid = user.userid;
                userobj.otp = OTP;
                db.user_otp.Add(userobj);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("edsins007@gmail.com");
                mail.To.Add(fp.email);
                mail.Subject = "Airlines OTP";
                mail.Body = OTP.ToString();

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("edsins007@gmail.com", "radioactive1!");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return Ok("mail Sent");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
