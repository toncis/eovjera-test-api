using eOvjera.Common;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eOvjera.Common.Cache
{
    /// <summary>
    /// eOvjera - Common - MyMemoryCache interface.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.Object, System.Object}}" />
    /// <seealso cref="Microsoft.Extensions.Caching.Memory.IMemoryCache" />
    public interface IMyMemoryCache : IEnumerable<KeyValuePair<object, object>>, IMemoryCache
    {
        /// <summary>
        /// Clears all cache entries.
        /// </summary>
        void ClearAllCache();

        /// <summary>
        /// Clears all user cache entries.
        /// </summary>
        void ClearAllUserCache();

        /// <summary>
        /// Clears all cache entries.
        /// </summary>
        void ClearAllOrgJedCache();

        /// <summary>
        /// Clears all cache entries.
        /// </summary>
        void ClearAllDocumentCache();

        /// <summary>
        /// Clears all choice entries.
        /// </summary>
        void ClearAllChoiceCache();
        /// <summary>
        /// Clears all workflow templates cache entries.
        /// </summary>
        void ClearAllWorkflowTemplatesCache();
    }
}
