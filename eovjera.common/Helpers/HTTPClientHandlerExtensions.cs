using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace eOvjera.Common.Helpers
{
    /// <summary>
    /// eOvjera - Common - HTTPClientHandler extension class.
    /// </summary>
    public static class HTTPClientHandlerExtensions
    {
        /// <summary>
        /// Set proxy settings to the http request handler from config settings.
        /// </summary>
        /// <param name="clientHandler"></param>
        public static void SetProxySettings(ref HttpClientHandler clientHandler, string useProxy, string proxyUrl, string bypassProxyOnLocal, string ProxyUsername, string proxyPassword)
        {
            if (string.IsNullOrEmpty(useProxy) || !bool.Parse(useProxy))
            {
                clientHandler = new HttpClientHandler() { UseProxy = false };
                return;
            }

            clientHandler = new HttpClientHandler()
            {
                Proxy = new WebProxy(proxyUrl)
                {
                    BypassProxyOnLocal = bool.Parse(bypassProxyOnLocal),
                    Credentials = new NetworkCredential(ProxyUsername, proxyPassword),
                },
                UseProxy = true,
            };
        }

        /// <summary>
        /// Set http request timeout from config settings.
        /// </summary>
        /// <returns>The http timeout.</returns>
        public static TimeSpan SetTimeout(string appSettTimeout)
        {
            if (!string.IsNullOrEmpty(appSettTimeout) && double.TryParse(appSettTimeout, out double timeout))
                return TimeSpan.FromSeconds(timeout);
            else
                return TimeSpan.FromSeconds(100); // The default value is 100 000 miliseconds (100 seconds) according to docs.microsoft.com
        }
    }
}
