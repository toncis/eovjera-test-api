using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eOvjera.Common.Email
{
    public interface IEmailService
    {
        void AddEmail(string eMailAddressTo, string fromMailAddress, string subject, string body, bool isHtmlBody);
        void AddEmail(List<string> eMailAddressTo, List<string> eMailAddressCc, List<string> eMailAddressBcc, string fromMailAddress, string subject, string body, bool isHtmlBody);
        void AddBulkEmail(string[] bccAddressList, string fromMailAddress, string subject, string body, bool isHtmlBody);
        Task Send();
    }
}
