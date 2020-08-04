using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eOvjera.Test.WebAPI.Controllers
{
    /// <summary>
    /// MVC - Base Controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class BaseController : Controller
    {
        /// <summary>
        /// The logger.
        /// </summary>
        public readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public BaseController(ILogger logger)
        {
            this._logger = logger;
        }
    }
}