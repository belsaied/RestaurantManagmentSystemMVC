using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.SendEmailService
{
    public  class SendEmailService:ISendEmailService
    {
        public void SendEmail(Email email)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress("shahdelfares515@gmail.com");
                message.To.Add(email.To);
                message.Subject = email.Subject;
                message.Body =email.Body;
                message.IsBodyHtml = true;

                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("shahdelfares515@gmail.com", "hgdvrcpkppzaatpj"); // Use your email and app password

                    client.Send(message);

                }
            }
                }
    }
}
