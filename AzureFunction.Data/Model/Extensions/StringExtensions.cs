using System;
using System.Diagnostics;
using System.Globalization;

namespace AzureFunction.Data.Model.Extensions
{
    [DebuggerStepThrough]
    public static class StringExtensions
    {
        /// <summary>
        /// Formats this string with the CurrentCulture passing in the specified parameters
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static string FormatCurrentCulture(this String format, params object[] values)
        {
            return string.Format(CultureInfo.CurrentCulture, format, values);
        }

        /// <summary>
        /// Indicates whether this string is null or empty
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this String value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Indicates whether this string is not null or empty
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [is not null or empty] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNotNullOrEmpty(this String value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}
