using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace eOvjera.Common.Helpers
{
    /// <summary>
    /// eOvjera - Common - DAL extension class.
    /// </summary>
    public static class DALExtensions
    {
        /// <summary>
        /// Safes the get string.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="colName">Name of the col.</param>
        /// <returns></returns>
        public static string SafeGetString(this DbDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);

            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetString(colIndex);
            }
            else
            {
                return null;
            }
        }
    }
}
