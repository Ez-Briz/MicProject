using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using API.Interfaces;
using Hangfire;

namespace API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendMessage(string email, string message, string subject)
        {
            try
            {
                var yourMail = _config["EmailSettings:Email"];
                var yourMailPassword = _config["EmailSettings:Password"];
                Console.WriteLine(yourMail);
                Console.WriteLine(yourMailPassword);
                MailAddress from = new(yourMail, "Projects Corp.");
                MailAddress to = new(email.ToLower());
                MailMessage mailMessage = new(from, to)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = message
                };
                SmtpClient smtp = new("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(yourMail, yourMailPassword)
                };
                await smtp.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}