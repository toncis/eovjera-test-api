using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOvjera.Test.WebAPI.Models.Notification
{
    /// <summary>
    /// WebAPI - Model - Notification view model.
    /// </summary>
    /// <seealso cref="eOvjera.Test.WebAPI.Models.Notification.INotification" />
    public class Notification : INotification

    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        public Notification()
        { 
            this.Message = String.Empty;
            this.Identifier = String.Empty;
            this.Code = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="identifier">The identifier.</param>
        public Notification(string message, string identifier)
        {
            this.Message = message;
            this.Identifier = identifier;
            this.Code = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="identifier">The identifier.</param>
        /// <param name="code">The code.</param>
        public Notification(string message, string identifier, string code)
        {
            this.Message = message;
            this.Identifier = identifier;
            this.Code = code;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code
        {
            get;
            set;
        }
    }
}
