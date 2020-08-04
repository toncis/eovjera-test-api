using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOvjera.Common
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Converter
    {
        private readonly Type from; // Type of the instance to convert.
        private readonly Type to;   // Type that the instance will be converted to.

        /// <summary>
        /// Initializes a new instance of the <see cref="Converter"/> class.
        /// Internal, because we'll provide the only implementation...
        /// ...that's also why we don't check if the arguments are null.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        internal Converter(Type from, Type to)
        {
            this.from = from;
            this.to = to;
        }

        /// <summary>
        /// Gets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public Type From { get { return this.from; } }
        /// <summary>
        /// Gets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public Type To { get { return this.to; } }

        /// <summary>
        /// Converts the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public abstract object Convert(object obj);
    }

    /// <summary>
    /// Sealed, because this is meant to be the only implementation.
    /// </summary>
    /// <typeparam name="TFrom">The type of from.</typeparam>
    /// <typeparam name="TTo">The type of to.</typeparam>
    public sealed class Converter<TFrom, TTo> : Converter
    {
        Func<TFrom, TTo> converter; // Converter is strongly typed.

        /// <summary>
        /// Initializes a new instance of the <see cref="Converter{TFrom, TTo}"/> class.
        /// </summary>
        /// <param name="converter">The converter.</param>
        /// <exception cref="ArgumentNullException">converter - Converter must not be null.</exception>
        public Converter(Func<TFrom, TTo> converter)
            : base(typeof(TFrom), typeof(TTo)) // Can't send null types to the base.
        {
            if (converter == null)
                throw new ArgumentNullException("converter", "Converter must not be null.");

            this.converter = converter;
        }

        /// <summary>
        /// Converts the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">obj</exception>
        public override object Convert(object obj)
        {
            if (!(obj is TFrom))
            {
                var msg = string.Format("Object is not of the type {0}.", this.From.FullName);
                throw new ArgumentException(msg, "obj");
            }

            // Can throw exception, it's ok.
            return this.converter.Invoke((TFrom)obj);
        }
    }



    /// <summary>
    /// Data type convertor helper class.
    /// </summary>
    public static class ConvertHelper
    {
        public static IEnumerable<Converter> Converters = new List<Converter>()
        {
            new Converter<short, string>(i => i.ToString()),
            new Converter<int, string>  (i => i.ToString()),
            new Converter<long, string> (i => i.ToString()),

            new Converter<string, short>(i => Convert.ToInt16(i, CultureInfo.CurrentCulture)),
            new Converter<string, int>  (i => Convert.ToInt32(i, CultureInfo.CurrentCulture)),
            new Converter<string, long> (i => Convert.ToInt64(i, CultureInfo.CurrentCulture)),

            new Converter<short, long>  (i => Convert.ToInt64(i)),
            new Converter<int, long>    (i => Convert.ToInt64(i)),
            new Converter<int, short>   (i => Convert.ToInt16(i)),
            new Converter<long, short>  (i => Convert.ToInt16(i)),
        };

        /// <summary>
        /// Tries the cast.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static bool TryCast<T>(
            this object obj,
            out T result)
        {
            if (obj is T)
            {
                result = (T)obj;
                return true;
            }

            // If it's null, we can't get the type.
            if (obj != null)
            {
                var converter = Converters.FirstOrDefault(c =>
                    c.From == obj.GetType() && c.To == typeof(T));

                // Use the converter if there is one.
                if (converter != null)
                    try
                    {
                        result = (T)converter.Convert(obj);
                        return true;
                    }
                    catch (Exception)
                    {
                        // Ignore - "Try*" methods don't throw exceptions.
                    }
            }

            result = default(T);
            return false;
        }


        /// <summary>
        /// To the long.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToLong(this int value)
        {
            return Convert.ToInt64(value);
        }

        /// <summary>
        /// To the short.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static short? ToShort(this long? value)
        {
            return value.HasValue ? Convert.ToInt16(value.Value) : (short?)null;
        }

        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte? ToByte(this long? value)
        {
            return value.HasValue ? Convert.ToByte(value.Value) : (byte?)null;
        }

        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte ToByte(this int value)
        {
            return Convert.ToByte(value);
        }

        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte ToByte(this int? value)
        {
            return Convert.ToByte(value);
        }

        /// <summary>
        /// Ints to short.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static short IntToShort(this int value)
        {
            return Convert.ToInt16(value);
        }

        /// <summary>
        /// To the short.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static short ToShort(this int? value)
        {
            return Convert.ToInt16(value);
        }

        /// <summary>
        /// To the short.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static short ToShort(this long value)
        {
            return Convert.ToInt16(value);
        }
    }
}
