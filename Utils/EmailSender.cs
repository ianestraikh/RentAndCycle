using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RentAndCycleCodeFirst.Utils
{
    public class EmailSender
    {
        public void Send(string fromEmail, List<string> toEmails, string subject, string contents, Stream file = null)
        {
            var client = new SendGridClient(Environment.GetEnvironmentVariable("SENDGRID_API", EnvironmentVariableTarget.User));
            var from = new EmailAddress(fromEmail, "Rent And Cycle");
            var to = toEmails.Select((e) => new EmailAddress(e)).ToList();
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var showAllRecipients = false;
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent, showAllRecipients);
            if (file != null)
            {
                msg.AddAttachmentAsync("attachment.pdf", file);
            }
            _ = client.SendEmailAsync(msg);
        }
    }
}