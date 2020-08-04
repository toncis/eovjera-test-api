using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOvjera.Test.Model
{
    /// <summary>
    /// Config - WebAPI envirorment settings.
    /// </summary>
    public class EnvirormentSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnvirormentSettings"/> class.
        /// </summary>
        public EnvirormentSettings()
        {
            this.ApplicationRootFolder = String.Empty;

            this.SwaggerEndpointName = String.Empty;
            this.SwaggerEndpointBaseUrl = String.Empty;
            this.SwaggerEndpointUrl = String.Empty;
            this.SwaggerXmlComments = String.Empty;

            this.WebApiVersion = String.Empty;
            this.WebApiTitle = String.Empty;
            this.WebApiDescription = String.Empty;
            this.WebApiContactMail = String.Empty;
            this.WebApiContactName = String.Empty;
            this.WebApiContactUrl = String.Empty;
            this.WebApiLicenseName = String.Empty;
            this.WebApiLicenseUrl = String.Empty;
            this.WebApiTermOfService = String.Empty;

            this.WebApiEnforceHTTPS = false;
        }
        
        /// <summary>
        /// Gets or sets the application root folder.
        /// </summary>
        /// <value>
        /// The application root folder.
        /// </value>
        public string ApplicationRootFolder { get; set; }
        /// <summary>
        /// Gets or sets the WebAPI hosting enforsing HTTPS.
        /// false - (default) No HTTPS required for hosting WebAPI
        /// true - Enforse HTTPS for hosting WebAPI
        /// </summary>
        /// <value>
        /// The hosting envirorment.
        /// </value>
        public bool WebApiEnforceHTTPS { get; set; }
        /// <summary>
        /// Gets or sets the Swagger Endpoint Name.
        /// </summary>
        /// <value>
        /// The Swagger Endpoint Name.
        /// </value>
        public string SwaggerEndpointName { get; set; }
        /// <summary>
        /// Gets or sets the Swagger Endpoint base URL.
        /// </summary>
        /// <value>
        /// The Swagger Endpoint base URL.
        /// </value>
        public string SwaggerEndpointBaseUrl { get; set; }
        /// <summary>
        /// Gets or sets the Swagger Endpoint URL.
        /// </summary>
        /// <value>
        /// The Swagger Endpoint URL.
        /// </value>
        public string SwaggerEndpointUrl { get; set; }
        /// <summary>
        /// Gets or sets the Swagger XML comment file.
        /// </summary>
        /// <value>
        /// The Swagger XML comment file.
        /// </value>
        public string SwaggerXmlComments { get; set; }


        /// <summary>
        /// Gets or sets the WebAPI version.
        /// </summary>
        /// <value>
        /// The WebAPI version.
        /// </value>
        public string WebApiVersion { get; set; }
        /// <summary>
        /// Gets or sets the WebAPI title.
        /// </summary>
        /// <value>
        /// The WebAPI title.
        /// </value>
        public string WebApiTitle { get; set; }
        /// <summary>
        /// Gets or sets the WebAPI description.
        /// </summary>
        /// <value>
        /// The WebAPI description.
        /// </value>
        public string WebApiDescription { get; set; }
        /// <summary>
        /// Gets or sets the WebAPI contact mail.
        /// </summary>
        /// <value>
        /// The WebAPI contact mail.
        /// </value>
        public string WebApiContactMail { get; set; }
        /// <summary>
        /// Gets or sets the WebAPI contact URL.
        /// </summary>
        /// <value>
        /// The WebAPI contact URL.
        /// </value>
        public string WebApiContactUrl { get; set; }
        /// <summary>
        /// Gets or sets the WebAPI contact name.
        /// </summary>
        /// <value>
        /// The WebAPI contact name.
        /// </value>
        public string WebApiContactName { get; set; }
        /// <summary>
        /// Gets or sets the WebAPI license name.
        /// </summary>
        /// <value>
        /// The WebAPI contact mail.
        /// </value>
        public string WebApiLicenseName { get; set; }
        /// <summary>
        /// Gets or sets the WebAPI license URL.
        /// </summary>
        /// <value>
        /// The WebAPI contact URL.
        /// </value>
        public string WebApiLicenseUrl { get; set; }
        /// <summary>
        /// Gets or sets the WebAPI term of service.
        /// </summary>
        /// <value>
        /// The WebAPI term of service.
        /// </value>
        public string WebApiTermOfService { get; set; }
    }
}
