using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Logging;

namespace eOvjera.Common.Email
{
    public class MailKitEmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;
        List<EmailContainerItem> _emailContainerItems;
        MailMessageHandler _mailMessageHandler;
        EmailAddress _fromMailAddress;
        IEmailBuilder _emailBuilder;
        private readonly ILogger _logger;

        public MailKitEmailService(IEmailConfiguration emailConfiguration, ILogger<MailKitEmailService> logger)
        {
            _emailConfiguration = emailConfiguration;
            _emailContainerItems = new List<EmailContainerItem>();
            _emailBuilder = new MailKitEmailBuilder();
            _fromMailAddress = new EmailAddress() { Address = emailConfiguration.FromMailAddress };
            _mailMessageHandler = new MailMessageHandler();
            _logger = logger;
        }

        private EmailContainerItem CreateItem(string fromMailAddress, string subject, string body, bool isHtmlBody)
        {
            var emailContainerItem = new EmailContainerItem();
            emailContainerItem.MailMessage = _mailMessageHandler.PrepareEmail(subject, body, isHtmlBody);
            emailContainerItem.EmailBuilder = _emailBuilder;
            if (fromMailAddress != null)
            {
                EmailAddress address = new EmailAddress() { Address = fromMailAddress };
                emailContainerItem.MailMessage.FromAddresses.Add(address);
            }
            else if (_fromMailAddress != null)
                emailContainerItem.MailMessage.FromAddresses.Add(_fromMailAddress);
            return emailContainerItem;
        }

        public void AddBulkEmail(string[] bccAddressList, string fromMailAddress, string subject, string body, bool isHtmlBody)
        {
            var emailContainerItem = CreateItem(fromMailAddress, subject, body, isHtmlBody);
            _mailMessageHandler.FillBcc(emailContainerItem.MailMessage, bccAddressList);
            _emailContainerItems.Add(emailContainerItem);
        }

        public void AddEmail(string eMailAddressTo, string fromMailAddress, string subject, string body, bool isHtmlBody)
        {
            var emailContainerItem = CreateItem(fromMailAddress, subject, body, isHtmlBody);
            emailContainerItem.MailMessage.ToAddresses.Add(new EmailAddress() { Address = eMailAddressTo });
            _emailContainerItems.Add(emailContainerItem);
        }

        public void AddEmail(List<string> eMailAddressTo, List<string> eMailAddressCc, List<string> eMailAddressBcc, string fromMailAddress, string subject, string body, bool isHtmlBody)
        {
            var emailContainerItem = CreateItem(fromMailAddress, subject, body, isHtmlBody);
            _mailMessageHandler.Fill(emailContainerItem.MailMessage.ToAddresses, eMailAddressTo);
            _mailMessageHandler.Fill(emailContainerItem.MailMessage.CcAddresses, eMailAddressCc);
            _mailMessageHandler.Fill(emailContainerItem.MailMessage.BccAddresses, eMailAddressBcc);

            _emailContainerItems.Add(emailContainerItem);
        }

        public async Task Send()
        {
            using (var emailClient = new SmtpClient())
            {
                //TODO remove callback function
                emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //The last parameter here is to use SSL (Which you should!)
                SecureSocketOptions secureSocketOption = SecureSocketOptions.StartTls;
                switch (_emailConfiguration.SmtpConnectionSecurity.ToLower())
                {
                    case "auto":
                        secureSocketOption = SecureSocketOptions.Auto;
                        break;
                    case "none":
                        secureSocketOption = SecureSocketOptions.None;
                        break;
                    case "sslonconnect":
                        secureSocketOption = SecureSocketOptions.SslOnConnect;
                        break;
                    case "starttls":
                        secureSocketOption = SecureSocketOptions.StartTls;
                        break;
                    case "starttlswhenavailable":
                        secureSocketOption = SecureSocketOptions.StartTlsWhenAvailable;
                        break;
                    default:
                        secureSocketOption = SecureSocketOptions.None;
                        break;
                }

                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, secureSocketOption);

                //Remove any OAuth functionality as we won't be using it. 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                switch (_emailConfiguration.SmtpAuthenticationMethod.ToLower())
                {
                    case "none":
                        break;
                    case "password":
                        emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                        break;
                    case "encryptedpassword":
                        emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                        break;
                    case "kerberos":
                        // To Do - Use identity
                        emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                        break;
                    case "ntlm":
                        // To Do - Use identity
                        emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                        break;
                    case "oauth2":
                        // To Do
                        emailClient.AuthenticationMechanisms.Add("XOAUTH2");
                        emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                        break;
                    default:
                        break;
                }

                foreach (var item in _emailContainerItems)
                {
                    var message = new MimeMessage();

                    if (item.EmailBuilder != null)
                    {
                        message = (MimeMessage)item.EmailBuilder.Build(item.MailMessage);
                    }

                    try
                    {
                        await emailClient.SendAsync(message);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.InnerException, "MailKitEmailService - Send() exception: " + message.ToString());
                    }
                }
                _emailContainerItems.Clear();
                
                emailClient.Disconnect(true);
            }
        }
    }
}
