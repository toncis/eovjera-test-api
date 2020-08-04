using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOvjera.Test.WebAPI.Models.Notification
{
    /// <summary>
    /// WebAPI - Model - 
    /// </summary>
    /// <seealso cref="eOvjera.Test.WebAPI.Models.Notification.INotificationContainer" />
    public abstract class aNotificationContainer : INotificationContainer
    {
        /// <summary>
        /// The errors
        /// </summary>
        protected List<INotification> _errors;
        /// <summary>
        /// The successes
        /// </summary>
        protected List<INotification> _successes;
        /// <summary>
        /// The warnings
        /// </summary>
        protected List<INotification> _warnings;

        /// <summary>
        /// Initializes a new instance of the <see cref="aNotificationContainer"/> class.
        /// </summary>
        public aNotificationContainer()
        {
            _errors = new List<INotification>();
            _successes = new List<INotification>();
            _warnings = new List<INotification>();
        }

        /// <summary>
        /// Gets a value indicating whether this instance has error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </value>
        public bool HasError
        {
            get { return _errors.Count > 0; }
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IEnumerable<INotification> Errors
        {
            get { return _errors.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        public IEnumerable<INotification> Success
        {
            get { return _successes.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the warnings.
        /// </summary>
        /// <value>
        /// The warnings.
        /// </value>
        public IEnumerable<INotification> Warnings
        {
            get { return _warnings.AsEnumerable(); }
        }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="notification">The notification.</param>
        public void AddError(INotification notification)
        {
            _errors.Add(notification);
        }

        /// <summary>
        /// Adds the success.
        /// </summary>
        /// <param name="notification">The notification.</param>
        public void AddSuccess(INotification notification)
        {
            _successes.Add(notification);
        }

        /// <summary>
        /// Adds the warning.
        /// </summary>
        /// <param name="notification">The notification.</param>
        public void AddWarning(INotification notification)
        {
            _warnings.Add(notification);
        }
    }
}
