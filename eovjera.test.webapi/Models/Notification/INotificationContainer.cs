using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOvjera.Test.WebAPI.Models.Notification
{
    /// <summary>
    /// WebAPI - Model - Notification container interface.
    /// </summary>
    public interface INotificationContainer
    {
        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="notification">The notification.</param>
        void AddError(INotification notification);
        /// <summary>
        /// Adds the success.
        /// </summary>
        /// <param name="notification">The notification.</param>
        void AddSuccess(INotification notification);
        /// <summary>
        /// Adds the warning.
        /// </summary>
        /// <param name="notification">The notification.</param>
        void AddWarning(INotification notification);

        /// <summary>
        /// Gets a value indicating whether this instance has error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </value>
        bool HasError { get; }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        IEnumerable<INotification> Errors { get; }
        /// <summary>
        /// Gets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        IEnumerable<INotification> Success { get; }
        /// <summary>
        /// Gets the warnings.
        /// </summary>
        /// <value>
        /// The warnings.
        /// </value>
        IEnumerable<INotification> Warnings { get; }
    }
}
