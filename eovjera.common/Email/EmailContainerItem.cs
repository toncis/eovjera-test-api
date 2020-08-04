namespace eOvjera.Common.Email
{
    /// <summary>
    /// EmailContainer internal item class for mailmessage queue
    /// </summary>
    public class EmailContainerItem
    {
        public EmailMessage MailMessage { get; set; }
        public IEmailBuilder EmailBuilder { get; set; } 
    }
}
