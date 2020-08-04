using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOvjera.Common
{
    /// <summary>
    /// Utils - class.
    /// </summary>
    public static class Utils {
        /// <summary>
        /// Determines whether [is null or empty].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified list]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list) {
            return !(list?.Any() ?? false);
        }

        /// <summary>
        /// Determines whether this instance is any.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>
        ///   <c>true</c> if the specified enumerable is any; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAny<T>(this IEnumerable<T> enumerable)
        {
            return enumerable?.Any() == true;
        }
    }
}