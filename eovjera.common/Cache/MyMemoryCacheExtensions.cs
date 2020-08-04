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
    /// Custom memory cache extensions methods class.
    /// </summary>
    public static class MyMemoryCacheExtensions
    {
        /// <summary>
        /// Set object value to cache.
        /// </summary>
        /// <param name="cache">The cache provider.</param>
        /// <param name="key">The object key.</param>
        /// <param name="value">The object value.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Cached object.</returns>
        public static T Set<T>(this IMyMemoryCache cache, object key, T value)
        {
            var entry = cache.CreateEntry(key);
            entry.Value = value;
            entry.SetAbsoluteExpiration(MyCacheParameters.CacheAbsoluteExpiration);
            entry.SetPriority(CacheItemPriority.Normal);
            entry.Dispose();

            return value;
        }

        /// <summary>
        /// Set object value to cache.
        /// </summary>
        /// <param name="cache">The cache provider.</param>
        /// <param name="key">The object key.</param>
        /// <param name="value">The object value.</param>
        /// <param name="priority">The object priority in cache.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Cached object.</returns>
        public static T Set<T>(this IMyMemoryCache cache, object key, T value, CacheItemPriority priority)
        {
            var entry = cache.CreateEntry(key);
            entry.SetAbsoluteExpiration(MyCacheParameters.CacheAbsoluteExpiration);
            entry.Priority = priority;
            entry.Value = value;
            entry.Dispose();

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// Set object value to cache.
        /// </summary>
        /// <param name="cache">The cache provider.</param>
        /// <param name="key">The object key.</param>
        /// <param name="value">The object value.</param>
        /// <param name="absoluteExpiration">The object cache absolute expiration time offset.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Cached object.</returns>
        public static T Set<T>(this IMyMemoryCache cache, object key, T value, DateTimeOffset absoluteExpiration)
        {
            var entry = cache.CreateEntry(key);
            entry.SetPriority(CacheItemPriority.Normal);
            entry.AbsoluteExpiration = absoluteExpiration;
            entry.Value = value;
            entry.Dispose();

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// Set object value to cache.
        /// </summary>
        /// <param name="cache">The cache provider.</param>
        /// <param name="key">The object key.</param>
        /// <param name="value">The object value.</param>
        /// <param name="absoluteExpirationRelativeToNow">The object cache absolute expiration time span from Now().</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Cached object.</returns>
        public static T Set<T>(this IMyMemoryCache cache, object key, T value, TimeSpan absoluteExpirationRelativeToNow)
        {
            var entry = cache.CreateEntry(key);
            entry.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            entry.SetPriority(CacheItemPriority.Normal);
            entry.Value = value;
            entry.Dispose();

            return value;
        }

        /// </summary>
        /// Set object value to cache.
        /// </summary>
        /// <param name="cache">The cache provider.</param>
        /// <param name="key">The object key.</param>
        /// <param name="value">The object value.</param>
        /// <param name="options">The cache provider enty options.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Cached object.</returns>
        public static T Set<T>(this IMyMemoryCache cache, object key, T value, MemoryCacheEntryOptions options)
        {
            using (var entry = cache.CreateEntry(key))
            {
                if (options != null)
                    entry.SetOptions(options);

                entry.Value = value;
            }

            return value;
        }

        /// <summary>
        /// Get and/or create object value from cache.
        /// </summary>
        /// <param name="cache">The cache provider.</param>
        /// <param name="key">The object key.</param>
        /// <param name="factory">The object provider function.</param>
        /// <typeparam name="TItem"></typeparam>
        /// <returns>Cached object.</returns>
        public static TItem GetOrCreate<TItem>(this IMyMemoryCache cache, object key, Func<ICacheEntry, TItem> factory)
        {
            if (!cache.TryGetValue(key, out var result))
            {
                var entry = cache.CreateEntry(key);
                result = factory(entry);
                entry.SetAbsoluteExpiration(MyCacheParameters.CacheAbsoluteExpiration);
                entry.SetPriority(CacheItemPriority.Normal);
                entry.SetValue(result);
                entry.Dispose();
            }

            return (TItem)result;
        }

        /// <summary>
        /// Get and/or create object value from cache async.
        /// </summary>
        /// <param name="cache">The cache provider.</param>
        /// <param name="key">The object key.</param>
        /// <param name="factory">The object provider function.</param>
        /// <typeparam name="TItem"></typeparam>
        /// <returns>Cached object.</returns>
        public static async Task<TItem> GetOrCreateAsync<TItem>(this IMyMemoryCache cache, object key, Func<ICacheEntry, Task<TItem>> factory)
        {
            if (!cache.TryGetValue(key, out object result))
            {
                var entry = cache.CreateEntry(key);
                result = await factory(entry);
                entry.SetAbsoluteExpiration(MyCacheParameters.CacheAbsoluteExpiration);
                entry.SetPriority(CacheItemPriority.Normal);
                entry.SetValue(result);
                entry.Dispose();
            }

            return (TItem)result;
        }

        /// <summary>
        /// Get and/or create object value from cache async.
        /// </summary>
        /// <param name="cache">The cache provider.</param>
        /// <param name="key">The object key.</param>
        /// <param name="absoluteExpirationRelativeToNow">The object cache absolute expiration time span from Now().</param>
        /// <param name="factory">The object provider function.</param>
        /// <typeparam name="TItem"></typeparam>
        /// <returns>Cached object.</returns>
        public static async Task<TItem> GetOrCreateAsync<TItem>(
            this IMyMemoryCache cache, 
            object key, 
            TimeSpan absoluteExpirationRelativeToNow, 
            Func<ICacheEntry, Task<TItem>> factory)
        {
            if (!cache.TryGetValue(key, out object result))
            {
                var entry = cache.CreateEntry(key);
                result = await factory(entry);
                entry.SetAbsoluteExpiration(absoluteExpirationRelativeToNow);
                entry.SetPriority(CacheItemPriority.Normal);
                entry.SetValue(result);
                entry.Dispose();
            }

            return (TItem)result;
        }

        /// <summary>
        /// Get and/or create object value from cache async.
        /// </summary>
        /// <param name="cache">The cache provider.</param>
        /// <param name="key">The object key.</param>
        /// <param name="absoluteExpirationRelativeToNow">The object cache absolute expiration time span from Now().</param>
        /// <param name="priority">The object priority in cache.</param>
        /// <param name="factory">The object provider function.</param>
        /// <typeparam name="TItem"></typeparam>
        /// <returns>Cached object.</returns>
        public static async Task<TItem> GetOrCreateAsync<TItem>(
            this IMyMemoryCache cache, 
            object key, 
            TimeSpan absoluteExpirationRelativeToNow,
            CacheItemPriority priority,
            Func<ICacheEntry, Task<TItem>> factory)
        {
            if (!cache.TryGetValue(key, out object result))
            {
                var entry = cache.CreateEntry(key);
                result = await factory(entry);
                entry.SetAbsoluteExpiration(absoluteExpirationRelativeToNow);
                entry.SetPriority(CacheItemPriority.Normal);
                entry.SetValue(result);
                entry.Dispose();
            }

            return (TItem)result;
        }
    }
}
