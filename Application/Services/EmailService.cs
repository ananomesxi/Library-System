using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail (string to, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient ("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("testemaillibrarysystem@gmail.com", "ibpx oikq mben johs");
            smtpClient.EnableSsl = true;

            MailMessage mailMessage = new MailMessage ("testemaillibrarysystem@gmail.com", to, subject, body);
            smtpClient.Send(mailMessage);
        }
    }
}
