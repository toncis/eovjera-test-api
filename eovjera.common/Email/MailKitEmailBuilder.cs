using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace eOvjera.Common.Email
{
    public class MailKitEmailBuilder : IEmailBuilder
    {
        public object Build(EmailMessage mail)
        {
            var message = new MimeMessage();
            message.To.AddRange(mail.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.Cc.AddRange(mail.CcAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.Bcc.AddRange(mail.BccAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(mail.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject = mail.Subject;

            if (mail.IsHtmlBody)
            {
                message.Body = new TextPart(TextFormat.Html) { Text = mail.Body };
            }
            else
            {
                message.Body = new TextPart(TextFormat.Plain) { Text = mail.Body };
            }

            return message;
        }
    }
}
