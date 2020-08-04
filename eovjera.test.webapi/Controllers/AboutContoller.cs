using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;
using System.Reflection;
using eOvjera.Test.WebApi;
using eOvjera.Test.Model;
using eOvjera.Test.WebAPI.Models;

namespace eOvjera.Test.WebAPI.Controllers
{
    /// <summary>
    /// eOvjera - Web API - MVC - About controller.
    /// </summary>
    /// <seealso cref="eOvjera.Test.WebAPI.Controllers.BaseController" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors(WebApiValues.CorsPolicyAll)]
    // [Authorize]
    public class AboutController : BaseController
    {
        private readonly EnvirormentSettings _appEnvirormentSettings;
        
        #region Class Contructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutController" /> class.
        /// </summary>
        /// <param name="appEnvirormentSettings">The application envirorment settings.</param>
        /// <param name="logger">The application logger.</param>
        public AboutController(
            IOptions<EnvirormentSettings> appEnvirormentSettings,
            ILogger<AboutController> logger
            ) : base(logger)
        {
            this._appEnvirormentSettings = appEnvirormentSettings.Value;
        }
        #endregion

        /// <summary>
        /// About. Gets the eOvjera Test WebAPI application about information object.
        /// </summary>
        /// <returns>A 200 OK response containing the eOvjera TEST WebAPI about object or a 404 Not Found if a about information was not found.</returns>
        /// <response code="200">The eOvjera TEST WebAPI about object.</response>
        /// <response code="401">The unauthorized request.</response>
        /// <response code="403">Forbidden : Access is denied.</response>
        /// <response code="404">Application about information could not be found.</response>
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(AboutResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AboutResponse>> AboutWebApi()
        {
            var retValue = new AboutResponse()
            {
                Application = _appEnvirormentSettings.WebApiTitle,
                Version = _appEnvirormentSettings.WebApiVersion,
                Os = RuntimeInformation.OSDescription,
                // Framework =  RuntimeInformation.FrameworkDescription,
                Framework = eOvjera.Common.AppAssemblyUtils.GetFrameworkDescription(),  
                Date = eOvjera.Common.AppAssemblyUtils.GetBuildDate(Assembly.GetExecutingAssembly()),
                WorkingFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            };

            return await Task.FromResult(retValue);
        }
    }
}