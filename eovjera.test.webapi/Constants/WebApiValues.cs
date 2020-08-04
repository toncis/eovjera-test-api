using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOvjera.Test.WebAPI
{
    /// <summary>
    /// Web API - Constants - main WebAPI values.
    /// </summary>
    public static class WebApiValues
    {
        /// <summary>
        /// The config section for WebAPI envirorment configuration section.
        /// </summary>
        public static readonly string WebApiEnvirormentConfiguration = "eOvjera.Test.Environment";

        /// <summary>
        /// The email configuration section
        /// </summary>
        public static readonly string ConfigSectionEmail = "eOvjera.Test.SMTP.server";

        /// <summary>
        /// The config section for JWT Audience parameters.
        /// </summary>
        public static readonly string ConfigSectionJwt = "eOvjera.Test.Audience";

        /// <summary>
        /// The app settings section for engine.
        /// </summary>
        public static readonly string ConfigSectionAppSettings = "eOvjera.Test.AppSettings";

        /// <summary>
        /// The app settings section for API CORS.
        /// </summary>
        public static readonly string ConfigSectionCors = "eOvjera.Test.Cors";

        /// <summary>
        /// The config section for JWT Audience parameters.
        /// </summary>
        public static readonly string ConfigSectionLdapSettings = "eOvjera.Test.LdapSettings";

        /// <summary>
        /// The config section for JWT Audience parameters.
        /// </summary>
        public static readonly string ConfigSectionApiEnvirormentSettings = "eOvjera.Test.Environment";

        /// <summary>
        /// The config section for document settings parameters.
        /// </summary>
        public static readonly string ConfigSectionApiDocumentSettings = "eOvjera.Test.Documents";

        /// <summary>
        /// The config section for document settings parameters.
        /// </summary>
        public static readonly string ConfigSectionApiDocumentCertificatesSettings = "eOvjera.Test.CertificateSettings";
        /// <summary>
        /// The config section for DMS.
        /// </summary>
        public static readonly string ConfigSectionDMSSettings = "eOvjera.Test.DMS.DMSSettings";

        /// <summary>
        /// The claim string for the all requests.
        /// </summary>
        public const string CorsPolicyAll = "AllowAll";

        /// <summary>
        /// The claim string for the local requests.
        /// </summary>
        public const string CorsPolicyLocal = "AllowLocal";

        /// <summary>
        /// The claim string for the eOvjera requests.
        /// </summary>
        public const string CorsPolicyOvjera = "AllowOvjera";


        /// <summary>
        /// The WebAPI application version.
        /// </summary>
        public static readonly string AppVersion = @"v1.0.";
        /// <summary>
        /// The WebAPI application title.
        /// </summary>
        public static readonly string AppTitle = @"eOvjera Test WebAPI.";
        /// <summary>
        /// The WebAPI application description.
        /// </summary>
        public static readonly string AppDescription = @"eOvjera Test WebAPI. API v1 - Tehnical documentation";
        /// <summary>
        /// The WebAPI application contact mail.
        /// </summary>
        public static readonly string AppContactMail = @"tonci.svilicic@inrebus.hr";
        /// <summary>
        /// The WebAPI application contact URL.
        /// </summary>
        public static readonly string AppContactUrl = @"https://github.com/toncis/eovjera-test-api";
        /// <summary>
        /// The WebAPI application name.
        /// </summary>
        public static readonly string AppContactName = @"Tonči Sviličić";
        /// <summary>
        /// The WebAPI application licence name.
        /// </summary>
        public static readonly string AppLicenceName = @"MIT License";
        /// <summary>
        /// The WebAPI application licence name.
        /// </summary>
        public static readonly string AppLicenceUrl = @"https://opensource.org/licenses/MIT";
        /// <summary>
        /// The WebAPI application term of licence.
        /// </summary>
        public static readonly string AppTermOfService = @"THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";
        

        /// <summary>
        /// The WebAPI application NoImage fuill file path.
        /// </summary>
        public static readonly string NoImageFilePath = @"noimage.png";


        // [Cache]

        /// <summary>
        /// The WebAPI application cache absolute expiration in hours.
        /// </summary>
        public static readonly double CacheAbsoluteExpirationInHours = 8;
        /// <summary>
        /// The WebAPI application cache absolute expiration.
        /// </summary>
        public static readonly TimeSpan CacheAbsoluteExpiration = TimeSpan.FromHours(CacheAbsoluteExpirationInHours);


        /// <summary>
        /// The WebAPI application cache sliding expiration in hours.
        /// </summary>
        public static readonly double CacheSlidingExpirationInHours = 8;
        /// <summary>
        /// The WebAPI application cache sliding expiration.
        /// </summary>
        public static readonly TimeSpan CacheSlidingExpiration = TimeSpan.FromHours(CacheSlidingExpirationInHours);
    }
}