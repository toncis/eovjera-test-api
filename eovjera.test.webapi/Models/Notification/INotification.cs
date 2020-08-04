using System;

namespace eOvjera.Test.WebAPI.Models.Notification
{
    /// <summary>
    /// WebAPI - Model - Notifications infterface.
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Identifier { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        string Code { get; set; }
    }
}
