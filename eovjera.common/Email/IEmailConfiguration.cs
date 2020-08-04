using System;
using System.Collections.Generic;
using System.Text;

namespace eOvjera.Common.Email
{
    public interface IEmailConfiguration
    {
        string SmtpServer { get; }
        int SmtpPort { get; }
        string SmtpUsername { get; set; }
        string SmtpPassword { get; set; }
        string SmtpUserDomain { get; set; }
        string SmtpConnectionSecurity { get; set; }
        string SmtpAuthenticationMethod { get; set; }
        string FromMailAddress { get; set; }

        string PopServer { get; }
        int PopPort { get; }
        string PopUsername { get; }
        string PopPassword { get; }
    }
}
