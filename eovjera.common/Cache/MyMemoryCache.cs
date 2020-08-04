using eOvjera.Common;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOvjera.Common.Cache
{
    /// <summary>
    /// eOvjera - Common - My memory cache class.
    /// </summary>
    /// <seealso cref="eOvjera.Common.Cache.IMyMemoryCache" />
    public class MyMemoryCache : IMyMemoryCache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<object, ICacheEntry> _cacheEntries = new ConcurrentDictionary<object, ICacheEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MyMemoryCache"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache.</param>
        public MyMemoryCache(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this._memoryCache.Dispose();
        }

        /// <summary>
        /// Posts the eviction callback.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="state">The state.</param>
        private void PostEvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            // Initaial 
            // if (reason != EvictionReason.Replaced)
            //     this._cacheEntries.TryRemove(key, out var _);
            
            // New version
            if (reason == EvictionReason.Removed)
                this._cacheEntries.TryRemove(key, out var _);
        }

        /// <summary>
        /// Gets the item associated with this key if present.
        /// </summary>
        /// <param name="key">An object identifying the requested entry.</param>
        /// <param name="value">The located value or null.</param>
        /// <returns>
        /// True if the key was found.
        /// </returns>
        /// <inheritdoc cref="IMemoryCache.TryGetValue" />
        public bool TryGetValue(object key, out object value)
        {
            return this._memoryCache.TryGetValue(key, out value);
        }

        /// <summary>
        /// Create or overwrite an entry in the cache and add key to Dictionary.
        /// </summary>
        /// <param name="key">An object identifying the entry.</param>
        /// <returns>
        /// The newly created <see cref="T:Microsoft.Extensions.Caching.Memory.ICacheEntry" /> instance.
        /// </returns>
        public ICacheEntry CreateEntry(object key)
        {
            var entry = this._memoryCache.CreateEntry(key);
            entry.RegisterPostEvictionCallback(this.PostEvictionCallback);
            this._cacheEntries.AddOrUpdate(key, entry, (o, cacheEntry) =>
            {
                cacheEntry.Value = entry;
                return cacheEntry;
            });
            return entry;
        }

        /// <summary>
        /// Removes the object associated with the given key.
        /// </summary>
        /// <param name="key">An object identifying the entry.</param>
        /// <inheritdoc cref="IMemoryCache.Remove" />
        public void Remove(object key)
        {
            this._memoryCache.Remove(key);
        }

        /// <summary>
        /// Clears all cache entries.
        /// </summary>
        /// <inheritdoc cref="IMyCache.Clear" />
        public void ClearAllCache()
        {
            foreach (var cacheEntry in this._cacheEntries.Keys.ToList())
                this._memoryCache.Remove(cacheEntry);
            
            foreach (var cacheEntry in this.ToList())
                this._memoryCache.Remove(cacheEntry.Key);
        }

        /// <summary>
        /// Clears all user cache entries.
        /// </summary>
        public void ClearAllUserCache()
        {
            foreach (var cacheEntry in this._cacheEntries.Keys.ToList())
            {
                if(cacheEntry.ToString().StartsWith(@"User_") || 
                   cacheEntry.ToString().StartsWith(@"UserRoleOrgJed_") ||
                   cacheEntry.ToString().StartsWith(@"Owner_") ||
                   cacheEntry.ToString().StartsWith(@"Role_") ||
                   cacheEntry.ToString().StartsWith(@"Choice_") || 
                   cacheEntry.ToString().StartsWith(@"SecurityContext_"))
                    this._memoryCache.Remove(cacheEntry);
            }

            foreach (var cacheEntry in this.ToList())
            {
                if(cacheEntry.Key.ToString().StartsWith(@"User_") || 
                   cacheEntry.Key.ToString().StartsWith(@"UserRoleOrgJed_") ||
                   cacheEntry.Key.ToString().StartsWith(@"Owner_") ||
                   cacheEntry.Key.ToString().StartsWith(@"Role_") ||
                   cacheEntry.Key.ToString().StartsWith(@"Choice_") || 
                   cacheEntry.Key.ToString().StartsWith(@"SecurityContext_"))
                    this._memoryCache.Remove(cacheEntry.Key);
            }
        }

        /// <summary>
        /// Clears all cache entries.
        /// </summary>
        public void ClearAllOrgJedCache()
        {
            foreach (var cacheEntry in this._cacheEntries.Keys.ToList())
            {
                if(cacheEntry.ToString().StartsWith(@"OrganizationUnit_") || 
                   cacheEntry.ToString().StartsWith(@"UserRoleOrgJed_") ||
                   cacheEntry.ToString().StartsWith(@"User_") ||
                   cacheEntry.ToString().StartsWith(@"Owner_") ||
                   cacheEntry.ToString().StartsWith(@"Role_") ||
                   cacheEntry.ToString().StartsWith(@"Choice_") ||
                   cacheEntry.ToString().StartsWith(@"SecurityContext_"))
                    this._memoryCache.Remove(cacheEntry);
            }

            foreach (var cacheEntry in this.ToList())
            {
                if(cacheEntry.Key.ToString().StartsWith(@"OrganizationUnit_") || 
                   cacheEntry.Key.ToString().StartsWith(@"UserRoleOrgJed_") ||
                   cacheEntry.Key.ToString().StartsWith(@"User_") ||
                   cacheEntry.Key.ToString().StartsWith(@"Owner_") ||
                   cacheEntry.Key.ToString().StartsWith(@"Role_") ||
                   cacheEntry.Key.ToString().StartsWith(@"Choice_") ||
                   cacheEntry.Key.ToString().StartsWith(@"SecurityContext_"))
                    this._memoryCache.Remove(cacheEntry.Key);
            }
        }

        /// <summary>
        /// Clears all cache entries.
        /// </summary>
        public void ClearAllDocumentCache()
        {
            foreach (var cacheEntry in this._cacheEntries.Keys.ToList())
            {
                if(cacheEntry.ToString().StartsWith(@"Document") ||
                   cacheEntry.ToString().StartsWith(@"AlfrescoFile"))
                    this._memoryCache.Remove(cacheEntry);
            }
            foreach (var cacheEntry in this.ToList())
            {
                if(cacheEntry.Key.ToString().StartsWith(@"Document") ||
                   cacheEntry.Key.ToString().StartsWith(@"AlfrescoFile"))
                    this._memoryCache.Remove(cacheEntry.Key);
            }
        }

        /// <summary>
        /// Clears all workflow templates cache entries.
        /// </summary>
        public void ClearAllWorkflowTemplatesCache()
        {
            foreach (var cacheEntry in this._cacheEntries.Keys.ToList())
            {
                if(cacheEntry.ToString().StartsWith(@"WorkflowTemplate_") ||
                   cacheEntry.ToString().StartsWith(@"FormGroupTemplate") ||
                   cacheEntry.ToString().StartsWith(@"FormLayoutTemplate") ||
                   cacheEntry.ToString().StartsWith(@"WorkflowStepItemTemplate") ||
                   cacheEntry.ToString().StartsWith(@"TaskTemplate_") ||
                   cacheEntry.ToString().StartsWith(@"WorkflowStepPlanTemplate") ||
                   cacheEntry.ToString().StartsWith(@"WorkflowStepTemplate") ||
                   cacheEntry.ToString().StartsWith(@"WorkflowTemplateViewUserCanCreate") ||
                   cacheEntry.ToString().StartsWith(@"ItemTemplate_") ||
                   cacheEntry.ToString().StartsWith(@"ContainerTemplate"))
                    this._memoryCache.Remove(cacheEntry);
            }

            foreach (var cacheEntry in this.ToList())
            {
                if(cacheEntry.Key.ToString().StartsWith(@"WorkflowTemplate_") ||
                   cacheEntry.Key.ToString().StartsWith(@"FormGroupTemplate") ||
                   cacheEntry.Key.ToString().StartsWith(@"FormLayoutTemplate") ||
                   cacheEntry.Key.ToString().StartsWith(@"WorkflowStepItemTemplate") ||
                   cacheEntry.Key.ToString().StartsWith(@"TaskTemplate_") ||
                   cacheEntry.Key.ToString().StartsWith(@"WorkflowStepPlanTemplate") ||
                   cacheEntry.Key.ToString().StartsWith(@"WorkflowStepTemplate") ||
                   cacheEntry.Key.ToString().StartsWith(@"WorkflowTemplateViewUserCanCreate") ||
                   cacheEntry.Key.ToString().StartsWith(@"ItemTemplate_") ||
                   cacheEntry.Key.ToString().StartsWith(@"ContainerTemplate"))
                    this._memoryCache.Remove(cacheEntry.Key);
            }
        }

        /// <summary>
        /// Clears all choice entries.
        /// </summary>
        public void ClearAllChoiceCache()
        {
            foreach (var cacheEntry in this._cacheEntries.Keys.ToList())
            {
                if(cacheEntry.ToString().StartsWith(@"ChoiceType_") || 
                   cacheEntry.ToString().StartsWith(@"Choice_"))
                    this._memoryCache.Remove(cacheEntry);
            }
            
            foreach (var cacheEntry in this.ToList())
            {
                if(cacheEntry.Key.ToString().StartsWith(@"ChoiceType_") || 
                   cacheEntry.Key.ToString().StartsWith(@"Choice_"))
                    this._memoryCache.Remove(cacheEntry.Key);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<object, object>> GetEnumerator()
        {
            return this._cacheEntries.Select(pair => new KeyValuePair<object, object>(pair.Key, pair.Value.Value)).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets keys of all items in MemoryCache.
        /// </summary>
        /// <value>
        /// The keys.
        /// </value>
        public IEnumerator<object> Keys => this._cacheEntries.Keys.GetEnumerator();
    }
}
