using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentAndCycleCodeFirst.Utils
{
    public class EmailSender
    {
        public void Send(string fromEmail, string subject, string contents)
        {
            var client = new SendGridClient(Environment.GetEnvironmentVariable("SENDGRID_API", EnvironmentVariableTarget.User));
            var from = new EmailAddress(fromEmail, "Customer Email Address");
            var to = new EmailAddress("iest0002@student.monash.edu", "Support Email Address");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            _ = client.SendEmailAsync(msg);
        }
    }
}