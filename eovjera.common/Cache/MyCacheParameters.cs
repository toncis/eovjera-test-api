using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOvjera.Common.Cache
{
    /// <summary>
    /// Cache parameters.
    /// </summary>
    public static class MyCacheParameters
    {
        // [Cache]

        /// <summary>
        /// The WebAPI application cache absolute expiration in hours.
        /// </summary>
        public static readonly double CacheAbsoluteExpirationInHours = 8;
        /// <summary>
        /// The WebAPI application cache absolute expiration.
        /// </summary>
        public static readonly TimeSpan CacheAbsoluteExpiration = TimeSpan.FromHours(CacheAbsoluteExpirationInHours);


        /// <summary>
        /// The WebAPI application cache sliding expiration in hours.
        /// </summary>
        public static readonly double CacheSlidingExpirationInHours = 8;
        /// <summary>
        /// The WebAPI application cache sliding expiration.
        /// </summary>
        public static readonly TimeSpan CacheSlidingExpiration = TimeSpan.FromHours(CacheSlidingExpirationInHours);
    }
}