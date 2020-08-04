using System.Collections.Generic;

namespace eOvjera.Common.Email
{
    public class MailMessageHandler
    {
        public EmailMessage PrepareEmail(
            string subject,
            string body,
            bool isHtmlBody)
        {
            EmailMessage mail = new EmailMessage();

            mail.Body = body;
            mail.Subject = subject;
            mail.IsHtmlBody = isHtmlBody;

            return mail;
        }

        public void FillBcc(EmailMessage mail, string[] bccAddressList)
        {
            foreach (string bcc in bccAddressList)
            {
                mail.BccAddresses.Add(new EmailAddress() { Address = bcc });
            }
        }

        public void Fill(List<EmailAddress> mailAddresses, List<string> addresses)
        {
            foreach (string address in addresses)
            {
                mailAddresses.Add(new EmailAddress() { Address = address });
            }
        }
    }
}
