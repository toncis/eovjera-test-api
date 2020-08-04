using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOvjera.Common
{
    /// <summary>
    /// Common - Logging - Custom logging event types.
    /// </summary>
    public class LoggingEvents
    {
        /// <summary>
        /// The items generated event.
        /// </summary>
        public const int GenerateItems = 1000;
        /// <summary>
        /// The item list event.
        /// </summary>
        public const int ListItems = 1001;
        /// <summary>
        /// The item get event.
        /// </summary>
        public const int GetItem = 1002;
        /// <summary>
        /// The item insert event.
        /// </summary>
        public const int InsertItem = 1003;
        /// <summary>
        /// The item update event.
        /// </summary>
        public const int UpdateItem = 1004;
        /// <summary>
        /// The item delete event.
        /// </summary>
        public const int DeleteItem = 1005;

        /// <summary>
        /// The item not found event.
        /// </summary>
        public const int GetItemNotFound = 4000;
        /// <summary>
        /// The Update item not found event.
        /// </summary>
        public const int UpdateItemNotFound = 4001;

        /// <summary>
        /// The BAL metadata item value cast exception event.
        /// </summary>
        public const int ItemValueCast = 5001;
        /// <summary>
        /// The BAL metadata LDAP user authenification process exception event.
        /// </summary>
        public const int LdapAuth = 5002;

    }
}