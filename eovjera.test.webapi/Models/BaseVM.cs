using eOvjera.Test.WebAPI.Models.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOvjera.Test.WebAPI.Models
{
    /// <summary>
    /// WebAPI - Model - Base view model.
    /// </summary>
    // [Serializable]
    // [DataContract]
    public class BaseVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseVM"/> class.
        /// </summary>
        public BaseVM()
        {
            this.NotificationContainer = new NotificationContainer();
        }

        /// <summary>
        /// Gets or sets the notification container.
        /// </summary>
        /// <value>
        /// The notification container.
        /// </value>
        // [DataMember]
        public NotificationContainer NotificationContainer { get; set; }

        /// <summary>
        /// Gets the BaseVM with notification error.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The BaseVM object with error message.</returns>
        public static BaseVM BaseVmError(string errorMessage)
        {
            var retValue = new BaseVM();
            retValue.NotificationContainer.AddError(errorMessage);
            return retValue;
        }

        /// <summary>
        /// Gets the BaseVM with notification warning.
        /// </summary>
        /// <param name="warningMessage">The warning message.</param>
        /// <returns>The BaseVM object with warning message.</returns>
        public static BaseVM BaseVmWarning(string warningMessage)
        {
            var retValue = new BaseVM();
            retValue.NotificationContainer.AddWarning(warningMessage);
            return retValue;
        }

        /// <summary>
        /// Gets the BaseVM with notification success.
        /// </summary>
        /// <param name="successMessage">The success message.</param>
        /// <returns>The BaseVM object with success message.</returns>
        public static BaseVM BaseVmSuccess(string successMessage)
        {
            var retValue = new BaseVM();
            retValue.NotificationContainer.AddSuccess(successMessage);
            return retValue;
        }
    }

    /// <summary>
    /// WebAPI - Model - Base view model extensions.
    /// </summary>
    public static class BaseVMExtensions
    {
        /// <summary>
        /// Sets the notification warning.
        /// </summary>
        /// <param name="vm">The this.</param>
        /// <param name="warningMessage">The warning message.</param>
        public static void SetWarning(this BaseVM vm, string warningMessage)
        {
            vm.NotificationContainer.AddWarning(warningMessage);
        }
        /// <summary>
        /// Sets the notification error.
        /// </summary>
        /// <param name="vm">The this.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void SetError(this BaseVM vm, string errorMessage)
        {
            vm.NotificationContainer.AddError(errorMessage);
        }
    }       
}
