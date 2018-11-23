using System.Diagnostics;

namespace AzureFunction.Data.Model.Extensions
{
    /// <summary>
    /// Additional extensions on <see cref="System.Object"/>
    /// </summary>
    [DebuggerStepThrough]
    public static class ObjectExtensions
    {
        /// <summary>
        /// Indicates whether this instance is not a null value
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [is not null] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNotNull(this object value)
        {
            return value != null;
        }

        /// <summary>
        /// Indicates whether this instance is a null value
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is null; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNull(this object value)
        {
            return value == null;
        }
    }
}