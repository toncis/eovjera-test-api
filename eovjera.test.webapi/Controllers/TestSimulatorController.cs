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
using Newtonsoft.Json.Linq;
using eOvjera.Test.Model;
using eOvjera.Test.WebAPI.Models;
using eOvjera.Common;

namespace eOvjera.Test.WebAPI.Controllers
{
    /// <summary>
    /// eOvjera - Web API - MVC - Test simulator controller.
    /// </summary>
    /// <seealso cref="eOvjera.Test.WebAPI.Controllers.BaseController" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors(WebApiValues.CorsPolicyAll)]
    // [Authorize]
    public class TestSimulatorController : BaseController
    {
        private readonly EnvirormentSettings _appEnvirormentSettings;
        
        #region Class Contructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TestSimulatorController" /> class.
        /// </summary>
        /// <param name="appEnvirormentSettings">The application envirorment settings.</param>
        /// <param name="logger">The application logger.</param>
        public TestSimulatorController(
            IOptions<EnvirormentSettings> appEnvirormentSettings,
            ILogger<TestSimulatorController> logger
            ) : base(logger)
        {
            this._appEnvirormentSettings = appEnvirormentSettings.Value;
        }
        #endregion

        /// <summary>
        /// Test simulator. Simulate successful GET request with JSON response for wfm test environments.
        /// </summary>
        /// <remarks>
        /// Used for wfm application testing.
        /// </remarks>
        /// <returns>
        /// A 200 OK response containing a json response.
        /// </returns>
        /// <response code="200">A json from the request data.</response>
        /// <response code="401">The unauthorized request.</response>
        /// <response code="403">Forbidden : Access is denied.</response>
        [HttpGet("TestGet/Successful")]
        [ProducesResponseType(typeof(JObject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseVM), StatusCodes.Status500InternalServerError)]
        public IActionResult SuccessfulGetTest()
        {
            _logger.LogInformation(LoggingEvents.GetItem, $"TestSimulatorController - SuccessfulGetTest() started");

            return new OkObjectResult(BaseVM.BaseVmSuccess(@"Uspješno testiran GET request with this JSON response."));
        }

        /// <summary>
        /// Test simulator. Simulate unsuccessful GET request with JSON response for wfm test environments.
        /// </summary>
        /// <remarks>
        /// Used for wfm application testing.
        /// </remarks>
        /// <returns>
        /// A 500 Server error response containing a json response.
        /// </returns>
        /// <response code="401">The unauthorized request.</response>
        /// <response code="403">Forbidden : Access is denied.</response>
        /// <response code="500">An internal server error on WebAPI.</response>
        [HttpGet("TestGet/Unsuccessful")]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseVM), StatusCodes.Status500InternalServerError)]
        public IActionResult UnsuccessfulGetTest()
        {
            _logger.LogInformation(LoggingEvents.GetItem, $"TestSimulatorController - UnsuccessfulGetTest() started");

            return StatusCode(500, BaseVM.BaseVmSuccess(@"Neuspješno testiran GET request with this JSON response."));
        }

        /// <summary>
        /// Test simulator. Simulate successful JSON POST request for wfm test environments.
        /// </summary>
        /// <remarks>
        /// Used for wfm application testing.
        /// </remarks>
        /// <param name="jsonRequest">The test JSON request data.</param>
        /// <returns>
        /// A 200 OK response containing a json from request, a 400 Bad Request if the json data request
        /// is invalid or a 404 Not Found if a json with the specified data was not generated.
        /// </returns>
        /// <response code="200">A json from the request data.</response>
        /// <response code="400">The json data request is invalid.</response>
        /// <response code="401">The unauthorized request.</response>
        /// <response code="403">Forbidden : Access is denied.</response>
        /// <response code="404">A json data was not created.</response>
        /// <response code="500">An internal server error on WebAPI.</response>
        [HttpPost("TestPost/Successful", Name = "SuccessfulPostJsonTest", Order = 1)]
        [ProducesResponseType(typeof(JObject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseVM), StatusCodes.Status500InternalServerError)]
        public IActionResult SuccessfulPostJsonTest([FromBody] JObject jsonRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(jsonRequest == null)
                return new OkObjectResult(BaseVM.BaseVmSuccess(@"Uspješno testiran prazan JSON request."));

            _logger.LogInformation(LoggingEvents.GetItem, $"TestSimulatorController - PostSuccessfulJsonTest() started for {jsonRequest.ToString(Newtonsoft.Json.Formatting.None)}).");

            return new OkObjectResult(jsonRequest);
        }
        
        /// <summary>
        /// Test simulator. Simulate unsuccessful json post request for wfm test environments.
        /// </summary>
        /// <remarks>
        /// Used for wfm application testing.
        /// </remarks>
        /// <param name="jsonRequest">The test JSON request data.</param>
        /// <returns>
        /// A 200 OK response containing a json from request, a 400 Bad Request if the json data request
        /// is invalid or a 404 Not Found if a json with the specified data was not generated.
        /// </returns>
        /// <response code="400">The json data request is invalid.</response>
        /// <response code="401">The unauthorized request.</response>
        /// <response code="403">Forbidden : Access is denied.</response>
        /// <response code="500">An internal server error on WebAPI.</response>
        [HttpPost("TestPost/Unsuccessful", Name = "UnsuccessfulPostJsonTest", Order = 2)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(JObject), StatusCodes.Status500InternalServerError)]
        public IActionResult UnsuccessfulPostJsonTest([FromBody] JObject jsonRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(jsonRequest == null)
                return StatusCode(500, BaseVM.BaseVmSuccess(@"Neuspješno testiran prazan JSON request."));

            _logger.LogInformation(LoggingEvents.GetItem, $"TestSimulatorController - PostUnsuccessfulJsonTest() started for {jsonRequest.ToString(Newtonsoft.Json.Formatting.None)}).");

            return StatusCode(500, jsonRequest);
        }
                /// <summary>
        /// Test simulator. Simulate successful JSON PUT request for wfm test environments.
        /// </summary>
        /// <remarks>
        /// Used for wfm application testing.
        /// </remarks>
        /// <param name="jsonRequest">The test JSON request data.</param>
        /// <returns>
        /// A 200 OK response containing a json from request, a 400 Bad Request if the json data request
        /// is invalid or a 404 Not Found if a json with the specified data was not generated.
        /// </returns>
        /// <response code="200">A json from the request data.</response>
        /// <response code="400">The json data request is invalid.</response>
        /// <response code="401">The unauthorized request.</response>
        /// <response code="403">Forbidden : Access is denied.</response>
        /// <response code="404">A json data was not created.</response>
        /// <response code="500">An internal server error on WebAPI.</response>
        [HttpPut("TestPut/Successful", Name = "SuccessfulPutJsonTest", Order = 3)]
        [ProducesResponseType(typeof(JObject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseVM), StatusCodes.Status500InternalServerError)]
        public IActionResult SuccessfulPutJsonTest([FromBody] JObject jsonRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(jsonRequest == null)
                return new OkObjectResult(BaseVM.BaseVmSuccess(@"Uspješno testiran prazan JSON PUT request."));

            _logger.LogInformation(LoggingEvents.GetItem, $"TestSimulatorController - SuccessfulPutJsonTest() started for {jsonRequest.ToString(Newtonsoft.Json.Formatting.None)}).");

            return new OkObjectResult(jsonRequest);
        }

        /// <summary>
        /// Test simulator. Simulate unsuccessful JSON PUT request for wfm test environments.
        /// </summary>
        /// <remarks>
        /// Used for wfm application testing.
        /// </remarks>
        /// <param name="jsonRequest">The test JSON request data.</param>
        /// <returns>
        /// A 200 OK response containing a json from request, a 400 Bad Request if the json data request
        /// is invalid or a 404 Not Found if a json with the specified data was not generated.
        /// </returns>
        /// <response code="400">The json data request is invalid.</response>
        /// <response code="401">The unauthorized request.</response>
        /// <response code="403">Forbidden : Access is denied.</response>
        /// <response code="500">An internal server error on WebAPI.</response>
        [HttpPut("TestPut/Unsuccessful", Name = "UnsuccessfulPutJsonTest", Order = 4)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(JObject), StatusCodes.Status500InternalServerError)]
        public IActionResult UnsuccessfulPutJsonTest([FromBody] JObject jsonRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(jsonRequest == null)
                return StatusCode(500, BaseVM.BaseVmSuccess(@"Neuspješno testiran prazan JSON PUT request."));

            _logger.LogInformation(LoggingEvents.GetItem, $"TestSimulatorController - UnsuccessfulPutJsonTest() started for {jsonRequest.ToString(Newtonsoft.Json.Formatting.None)}).");

            return StatusCode(500, jsonRequest);
        }

        /// <summary>
        /// Test simulator. Simulate successful JSON Delete request for wfm test environments.
        /// </summary>
        /// <param name="jsonRequest">The test JSON request data.</param>
        /// <returns>A 200 OK response containing JSON response based on request data.</returns>
        /// <response code="200">The action result information.</response>
        /// <response code="401">The unauthorized request.</response>
        /// <response code="403">Forbidden : Access is denied.</response>
        [HttpDelete("TestDelete/Successful", Name = "SuccessfulDeleteTest", Order = 5)]
        [ProducesResponseType(typeof(JObject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public IActionResult SuccessfulDeleteTest([FromBody] JObject jsonRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(jsonRequest == null)
                return new OkObjectResult(BaseVM.BaseVmSuccess(@"Uspješno testiran prazan JSON DELETE request."));

            _logger.LogInformation(LoggingEvents.GetItem, $"TestSimulatorController - SuccessfulDeleteTest() started for {jsonRequest.ToString(Newtonsoft.Json.Formatting.None)}).");

            return new OkObjectResult(jsonRequest);
        }



        /// <summary>
        /// Test simulator. Simulate unsuccessful JSON Delete request for wfm test environments.
        /// </summary>
        /// <param name="jsonRequest">The test JSON request data.</param>
        /// <returns>A 200 OK response containing JSON response based on request data.</returns>
        /// <response code="400">The json data request is invalid.</response>
        /// <response code="401">The unauthorized request.</response>
        /// <response code="403">Forbidden : Access is denied.</response>
        /// <response code="500">An internal server error on WebAPI.</response>
        [HttpDelete("TestDelete/Unsuccessful", Name = "UnsuccessfulDeleteTest", Order = 6)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(JObject), StatusCodes.Status500InternalServerError)]
        public IActionResult UnsuccessfulDeleteTest([FromBody] JObject jsonRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(jsonRequest == null)
                return StatusCode(500, BaseVM.BaseVmSuccess(@"Neuspješno testiran prazan JSON DELETE request."));

            _logger.LogInformation(LoggingEvents.GetItem, $"TestSimulatorController - UnsuccessfulDeleteTest() started for {jsonRequest.ToString(Newtonsoft.Json.Formatting.None)}).");

            return StatusCode(500, jsonRequest);
        }
    }
}