using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using eOvjera.Test.WebAPI.ViewModelschemaFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace eOvjera.Test.WebAPI.Models
{
    /// <summary>
    /// WebAPI - Model - API about response model.
    /// </summary>
    [Serializable]
    [DataContract]
    [SwaggerSchemaFilter(typeof(AboutResponseSchemaFilter))]
    public class AboutResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutResponse"/> class.
        /// </summary>
        public AboutResponse()
        {
            this.Application = String.Empty;
            this.Version = String.Empty;
            this.Os = String.Empty;
            this.Framework = String.Empty;
            this.Date = null;
            this.WorkingFolder = String.Empty;
        }

        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        /// <value>
        /// The application name.
        /// </value>
        [DataMember]
        public string Application { get; set; }
        /// <summary>
        /// Gets or sets the application version.
        /// </summary>
        /// <value>
        /// The application version.
        /// </value>
        [DataMember]
        public string Version { get; set; }
        /// <summary>
        /// Gets or sets the application date.
        /// </summary>
        /// <value>
        /// The application date.
        /// </value>
        [DataMember]
        public DateTime? Date { get; set; }
        /// <summary>
        /// Gets or sets the application OS description.
        /// </summary>
        /// <value>
        /// The application OS description.
        /// </value>
        [DataMember]
        public string Os { get; set; }
        /// <summary>
        /// Gets or sets the application .NET framework description.
        /// </summary>
        /// <value>
        /// The application .NET framework description.
        /// </value>
        [DataMember]
        public string Framework { get; set; }
        /// <summary>
        /// Gets or sets the application working folder.
        /// </summary>
        /// <value>
        /// The application working folder.
        /// </value>
        [DataMember]
        public string WorkingFolder { get; set; }
    }
}
