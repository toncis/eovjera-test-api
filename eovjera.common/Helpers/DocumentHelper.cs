using System;
using System.Collections.Generic;
using System.Text;

namespace eOvjera.Common.Helpers
{
    /// <summary>
    /// eOvjera - Common - Document helper class.
    /// </summary>
    public class DocumentHelper
    {
        /// <summary>
        /// Base64s the decode.
        /// </summary>
        /// <param name="base64EncodedData">The base64 encoded data.</param>
        /// <returns></returns>
        public static byte[] Base64Decode(string base64EncodedData)
        {
            return Convert.FromBase64String(base64EncodedData);
        }

        /// <summary>
        /// Converts to base64string.
        /// </summary>
        /// <param name="base64DecodedData">The base64 decoded data.</param>
        /// <returns></returns>
        public static string ToBase64String(byte[] base64DecodedData)
        {
            return Convert.ToBase64String(base64DecodedData);
        }
    }
}
