using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace eOvjera.Common.Helpers
{
    /// <summary>
    /// eOvjera - Common - Expand objects.
    /// </summary>
    public static class ExpandoObjectHelpers
    {
        /// <summary>
        /// Adds the property.
        /// </summary>
        /// <param name="expando">The expando.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
    }
}
