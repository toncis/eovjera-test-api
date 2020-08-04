using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace eOvjera.Common.Mapping
{
    /// <summary>
    /// Creates instances of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type with a parameterless constructor.</typeparam>
    public static class Factory<T>
        where T : new()
    {
        private static readonly Func<T> CreateInstanceFunc =
            Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();

        /// <summary>
        /// Creates an instance of type <typeparamref name="T"/> by calling it's parameterless constructor.
        /// </summary>
        /// <returns>An instance of type <typeparamref name="T"/>.</returns>
        public static T CreateInstance() => CreateInstanceFunc();
    }
}
