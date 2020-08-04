using System;
using System.Collections.Generic;
using System.Text;

namespace eOvjera.Common.Models
{
    public class ValidationModel
    {
        public ValidationModel()
        {
            Items = new List<ValidationItem>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<ValidationItem> Items { get; set; }
    }

    public class ValidationItem
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The Item template Id.
        /// </value>
        public int ItemTemplateId { get; set; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The Value.
        /// </value>
        public object Value { get; set; }
    }
}
