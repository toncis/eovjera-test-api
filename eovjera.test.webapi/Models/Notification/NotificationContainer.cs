using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOvjera.Test.WebAPI.Models.Notification
{
    /// <summary>
    /// Notification container using Notification class
    /// </summary>
    /// <seealso cref="eOvjera.Test.WebAPI.Models.Notification.aNotificationContainer" />
    /// <seealso cref="eOvjera.Test.WebAPI.Models.Notification.INotificationContainerExtension" />
    public class NotificationContainer:aNotificationContainer, INotificationContainerExtension
    {
        /// <summary>
        /// Creates the notification.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="identifier">The identifier.</param>
        /// <returns></returns>
        private Notification CreateNotification(string errorMessage, string identifier)
        {
            return new Notification(errorMessage, identifier);
        }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public void AddError(string errorMessage)
        {
            _errors.Add(CreateNotification(errorMessage,null));
        }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="identifier">The identifier.</param>
        public void AddError(string errorMessage, string identifier)
        {
            _errors.Add(CreateNotification(errorMessage, identifier));
        }

        /// <summary>
        /// Adds the success.
        /// </summary>
        /// <param name="successMessage">The success message.</param>
        public void AddSuccess(string successMessage)
        {
            _successes.Add(CreateNotification(successMessage, null));
        }

        /// <summary>
        /// Adds the warning.
        /// </summary>
        /// <param name="warningMessage">The warning message.</param>
        public void AddWarning(string warningMessage)
        {
            _warnings.Add(CreateNotification(warningMessage, null));
        }
    }
}
